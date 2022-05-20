using DG.Tweening;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIFade : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 2f)] private float transistionDuration = 0.4f;
        [SerializeField] private GameObject loadingGameObject;

        private void Start() => FadeOut();

        public void FadeOut()
        {
            loadingGameObject.SetActive(false);
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, transistionDuration);
        }

        public void FadeIn()
        {
            loadingGameObject.SetActive(true);
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, transistionDuration);
        }
    }
}
