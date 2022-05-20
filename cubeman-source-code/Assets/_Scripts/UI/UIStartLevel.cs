using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIStartLevel : MonoBehaviour
    {
        public delegate void MessageEnd();
        public event MessageEnd OnMessageEnd;

        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fadeDuration = 0.6f;
        [SerializeField] [Range(1f, 12f)] private float messageDuration = 4f;
        [SerializeField] [Range(0.1f, 2f)] private float messageDelay = 1f;

        private void Start() => StartCoroutine(MessageCoroutine());

        IEnumerator MessageCoroutine()
        {
            FadeIn();
            yield return new WaitForSeconds(messageDuration);
            FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            if (OnMessageEnd != null) { OnMessageEnd(); }
        }

        public void FadeIn()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration).SetDelay(messageDelay);
        }

        public void FadeOut()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration);
        }
    }
}
