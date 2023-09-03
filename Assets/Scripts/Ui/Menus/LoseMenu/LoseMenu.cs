using DG.Tweening;
using UnityEngine;

namespace Ui.Menus.LoseMenu
{
    public class LoseMenu : MonoBehaviour
    {
        [SerializeField] private Transform _menu;
        [SerializeField] private AudioSource _loseAudio, _music;
        private InputSystem _input;
        
        public void Init(InputSystem input)
        {
            _input = input;
            Enemy.Enemy[] enemies = FindObjectsOfType<Enemy.Enemy>();
            foreach (var enemy in enemies) enemy.Lost += Open;
            FindObjectOfType<Chat.Chat>().Lost += Open;
        }

        private void Open()
        {
            _menu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            _menu.DOLocalMoveY(0, 1).SetEase(Ease.OutElastic);
            _input.Disable();
            
            _music.Stop();
            _loseAudio.Play();
        }
    }
}