using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ui.Chat
{
    public class Chat : MonoBehaviour
    {
        [SerializeField] private string[] _textsToPost;
        [SerializeField] private TMP_Text _textBlockPrefab;
        [SerializeField] private TMP_InputField _answerField;
        [SerializeField] private Transform _content;

        [SerializeField] private AudioSource _notificationAudio;
        [SerializeField] private AudioSource _messageAudio;
        
        private InputSystem _input;
        private byte _currentText;
        private float _timeToPostText;
        private float _timeToAnswer;
        
        private bool _isWaitingAnswer;
        private bool _isOpened;
        
        public event Action Lost;

        public void Init(InputSystem input)
        {
            _input = input;
            _input.Chat.SendAnswer.performed += _ => SendAnswer();
            _input.Chat.OpenChat.performed += _ => OpenOrClose();
            _input.Ui.Enable();
        }

        private void FixedUpdate()
        {
            if (_currentText == _textsToPost.Length - 1) return;
            
            if (_isWaitingAnswer)
            {
                _timeToAnswer += Time.fixedDeltaTime;
                if (_timeToAnswer >= 6) Lost?.Invoke();
            }
            else
            {
                _timeToPostText += Time.fixedDeltaTime;
                if (_timeToPostText < 15) return;
                
                TMP_Text text = GetNewText();
                text.text = _textsToPost[_currentText];
                text.color = Color.magenta;
                
                _currentText++;
                _isWaitingAnswer = true;
                _timeToPostText = 0;
                if (_isOpened) _messageAudio.Play();
                else _notificationAudio.Play();
            }
        }

        private void SendAnswer()
        {
            if (!_isOpened) return;
            if (_answerField.text.Length == 0) return;
            
            GetNewText().text = _answerField.text;
            _answerField.text = default;
            _isWaitingAnswer = false;
            _timeToAnswer = 0;
            _messageAudio.Play();
        }

        private void OpenOrClose()
        {
            _isOpened = !_isOpened;
            if (_isOpened)
            {
                Cursor.lockState = CursorLockMode.None;
                _input.Player.Disable();
                transform.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutQuad);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                _input.Player.Enable();
                transform.DOLocalMoveY(-1000, 0.2f).SetEase(Ease.InQuad);
            }
        }

        private TMP_Text GetNewText() => Instantiate(_textBlockPrefab, _content);
    }
}