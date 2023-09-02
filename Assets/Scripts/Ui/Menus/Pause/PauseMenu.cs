using System;
using DG.Tweening;
using UnityEngine;

namespace Ui.Menus.Pause
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Transform _menu;
        private bool _isOpened;
        
        public void Init(InputSystem input) => input.Ui.Pause.performed += _ => OpenOrClose();

        public void OpenOrClose()
        {
            _isOpened = !_isOpened;
            if (_isOpened) Close(isActive: true);
            else Close(CursorLockMode.Locked, timeScale: 1);
        }

        private void Close(CursorLockMode cursorLockMode = CursorLockMode.None, bool isActive = false, byte timeScale = 0)
        {
            Cursor.lockState = cursorLockMode;
            _menu.gameObject.SetActive(isActive);
            Time.timeScale = timeScale;
        }
    }
}