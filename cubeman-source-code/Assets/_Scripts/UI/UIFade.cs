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
        [SerializeField] [Range(0.1f, 2f)] private float transistionDuration = 0.4f;
        [SerializeField] private GameObject loadingGameObject;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            Instance = this;
        }

        private void Start() => FadeOut();

        public void FadeOut()
        {
            loadingGameObject.SetActive(false);
            Fade(0f);
        }

        public void FadeIn()
        {
            loadingGameObject.SetActive(false);
            Fade(1f);
        }

        public void LoadingFadeIn()
        {
            loadingGameObject.SetActive(true);
            Fade(1f);
        }

        private void Fade(float endValue)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(endValue, transistionDuration).SetUpdate(true);
        }
    }
}
