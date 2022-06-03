using DG.Tweening;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIPauseMenu : MonoBehaviour
    {
        public delegate void PauseEnd();
        public event PauseEnd OnPauseEnd;

        public delegate void UnPauseEnd();
        public event UnPauseEnd OnUnPauseEnd;

        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fadeDuration = 0.6f;

        public void FadeIn()
        {
            canvasGroup.DOKill();

            gameObject.SetActive(true);

            canvasGroup.DOFade(1f, fadeDuration).SetUpdate(true).OnComplete(StartPause);
        }

        private void StartPause()
        {
            OnPauseEnd?.Invoke();
        }

        public void FadeOut()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(FinishPause);
        }

        private void FinishPause()
        {
            OnUnPauseEnd?.Invoke();
            gameObject.SetActive(false);
        }
    }
}