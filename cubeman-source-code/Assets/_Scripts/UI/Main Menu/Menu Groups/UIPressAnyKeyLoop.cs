using TMPro;
using DG.Tweening;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIPressAnyKeyLoop : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private TextMeshProUGUI pressAnyKeyTMP;

        [Header("Settings")]
        [SerializeField] [Range(0.4f, 2f)] private float fadeDuration = 1f;

        internal void PressedAnyKey()
        {
            pressAnyKeyTMP.DOKill();
            pressAnyKeyTMP.DOFade(1f, 0.4f);
        }

        internal void StartPressAnyKeyLoop()
        {
            pressAnyKeyTMP.DOFade(0f, fadeDuration).OnComplete(FadeIn);
        }

        private void FadeIn()
        {
            pressAnyKeyTMP.DOFade(1f, fadeDuration).OnComplete(FadeOut);
        }

        private void FadeOut()
        {
            pressAnyKeyTMP.DOFade(0f, fadeDuration).OnComplete(FadeIn);
        }
    }
}
