using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIWhiteFlash : MonoBehaviour
    {
        #region Singleton
        public static UIWhiteFlash Instance;
        #endregion

        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float defaultFlashStart = 0.4f;
        [SerializeField] [Range(0.1f, 1f)] private float defaultFlashWait = 1f;
        [SerializeField] [Range(0.1f, 2f)] private float defaultFlashEnd = 1.8f;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float bossDeathFlashStart = 0.4f;
        [SerializeField] [Range(0.1f, 1f)] private float bossDeathFlashWait = 1f;
        [SerializeField] [Range(0.1f, 2f)] private float bossDeathFlashEnd = 1.8f;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            Instance = this;
        }

        public void BossDeathFlash()
        {
            canvasGroup.DOFade(1f, bossDeathFlashStart).OnComplete(BossDeathWait);
        }

        private void BossDeathWait()
        {
            StopCoroutine(BossDeathWaitCoroutine());
            StartCoroutine(BossDeathWaitCoroutine());
        }

        IEnumerator BossDeathWaitCoroutine()
        {
            yield return new WaitForSeconds(bossDeathFlashWait);

            canvasGroup.DOFade(0f, bossDeathFlashEnd);
        }

        public void DoFlash()
        {
            canvasGroup.DOFade(1f, defaultFlashStart).OnComplete(DoFlashWait);
        }

        public void DoFlash(float flashStartDuration)
        {
            canvasGroup.DOFade(1f, flashStartDuration).OnComplete(DoFlashWait);
        }

        private void DoFlashWait()
        {
            StopCoroutine(FlashWaitCoroutine());
            StartCoroutine(FlashWaitCoroutine());
        }

        IEnumerator FlashWaitCoroutine()
        {
            yield return new WaitForSeconds(defaultFlashWait);

            canvasGroup.DOFade(0f, defaultFlashEnd);
        }
    }
}