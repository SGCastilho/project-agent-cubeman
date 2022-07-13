using DG.Tweening;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIFade : MonoBehaviour
    {
        #region Singleton
        public static UIFade Instance;
        #endregion

        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] private GameObject loadingGameObject;

        [Space(6)]

        [SerializeField] [Range(0.1f, 2f)] private float fadeInTransistionDuration = 0.4f;
        [SerializeField] [Range(0.1f, 2f)] private float fadeOutTransistionDuration = 0.4f;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            Instance = this;
        }

        private void Start() => FadeOut();

        public void FadeOut()
        {
            loadingGameObject.SetActive(false);
            Fade(0f, fadeOutTransistionDuration);
        }

        public void FadeIn()
        {
            loadingGameObject.SetActive(false);
            Fade(1f, fadeInTransistionDuration);
        }

        public void LoadingFadeIn()
        {
            loadingGameObject.SetActive(true);
            Fade(1f, fadeInTransistionDuration);
        }

        private void Fade(float endValue, float duration)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(endValue, duration).SetUpdate(true);
        }
    }
}
