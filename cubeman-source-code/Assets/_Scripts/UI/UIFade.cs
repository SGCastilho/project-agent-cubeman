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
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, transistionDuration).SetUpdate(true);
        }

        public void FadeIn()
        {
            loadingGameObject.SetActive(false);
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, transistionDuration).SetUpdate(true);;
        }

        public void LoadingFadeIn()
        {
            loadingGameObject.SetActive(true);
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, transistionDuration).SetUpdate(true);
        }
    }
}
