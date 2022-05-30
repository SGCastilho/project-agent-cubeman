using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIBossHUD : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Space(12)]

        [SerializeField] private Image healthBar;
        [SerializeField] private Image healthBarProgressive;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fadeDuration = 1f;

        [Space(12)]

        [SerializeField] [Range(0.1f, 2f)] private float progressiveBarDuration = 1f;

        public void HUDFadeIn()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration);
        }

        public void HUDFadeOut()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration);
        }

        public void RecoveryHealthBar(float currentHealth, float maxHealth)
        {
            HealthBarTween(ref currentHealth, ref maxHealth, ref healthBarProgressive, ref healthBar);
        }

        public void DamageHealthBar(float currentHealth, float maxHealth)
        {
            HealthBarTween(ref currentHealth, ref maxHealth, ref healthBar, ref healthBarProgressive);
        }

        private void HealthBarTween(ref float currentHealth, ref float maxHealth, ref Image linearBar, ref Image progressiveBar)
        {
            float convertValue = currentHealth / maxHealth;

            linearBar.DOKill();
            linearBar.fillAmount = convertValue;

            progressiveBar.DOKill();
            progressiveBar.DOFillAmount(convertValue, progressiveBarDuration);
        }
    }
}