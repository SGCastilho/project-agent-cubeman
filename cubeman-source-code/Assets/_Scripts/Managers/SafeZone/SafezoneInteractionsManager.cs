using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class SafezoneInteractionsManager : MonoBehaviour
    {
        public delegate void ShowInteractionsWindow();
        public event ShowInteractionsWindow OnShowInteractionsWindow;

        public delegate void HideInteractionWindow();
        public event HideInteractionWindow OnHideInteractionWindow;

        public void ShowInteractions()
        {
            if (GamePauseManager.Instance.GamePaused) return;

            GamePauseManager.Instance.BlockPause = true;

            OnShowInteractionsWindow?.Invoke();
        }

        public void HideInteractions()
        {
            OnHideInteractionWindow?.Invoke();

            GamePauseManager.Instance.BlockPause = false;
        }
    }
}
