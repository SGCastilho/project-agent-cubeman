using DG.Tweening;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIMainMenuGroup : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float canvasFadeInDuration = 1f;

        public void MainMenuStartTween()
        {
            FadeIn(canvasGroup, canvasFadeInDuration, EnableMainMenuRaycast);
        }

        private void EnableMainMenuRaycast()
        {
            canvasGroup.blocksRaycasts = true;
        }

        private void FadeIn(CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, duration);
        }

        private void FadeIn(CanvasGroup canvasGroup, float duration, TweenCallback action)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, duration).OnComplete(action);
        }
    }
}
