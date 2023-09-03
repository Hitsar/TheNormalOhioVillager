using DG.Tweening;
using UnityEngine;

namespace Ui.MainMenu.Settings
{
    public class SettingMenuAnimation : MonoBehaviour
    {
        private void OnEnable() => transform.DOLocalMoveY(0, 0.8f).SetEase(Ease.OutBack);

        public void Disable() => transform.DOLocalMoveY(-900, 0.6f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
    }
}