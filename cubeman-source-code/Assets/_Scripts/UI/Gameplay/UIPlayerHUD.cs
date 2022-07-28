using TMPro;
using DG.Tweening;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIPlayerHUD : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Space(12)]

        [SerializeField] private Image healthBar;
        [SerializeField] private Image healthBarProgressive;

        [Space(12)]

        [SerializeField] private Image ultimateBar;
        [SerializeField] private Image ultimateProgressive;

        [Space(12)]

        [SerializeField] private CanvasGroup resourcesCanvasGroup;
        [SerializeField] private TextMeshProUGUI resourcesTMP;
        [SerializeField] private CanvasGroup amountResourcesCanvasGroup;
        [SerializeField] private TextMeshProUGUI amountResourcesTMP;

        private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fadeDuration = 1f;
        [SerializeField] [Range(0.1f, 1f)] private float shakeDuration = 0.6f;
        [SerializeField] [Range(10f, 100f)] private float shakeStrength = 50;

        [Space(12)]

        [SerializeField] [Range(0.1f, 2f)] private float progressiveBarDuration = 1f;

        [Space(12)]

        [SerializeField] [Range(0.1f, 1f)] private float resourcesFadeDuration = 0.6f;
        [SerializeField] [Range(2f, 20f)] private float resourcesScreenTime = 16f;

        private float _currentResourcesScreenTime;
        private bool _resourcesInScreen;

        [SerializeField] [Range(0.1f, 1f)] private float amountResourcesFadeDuration = 0.4f;
        [SerializeField] [Range(2f, 20f)] private float amountResourcesScreenTime = 8f;

        private StringBuilder _amountResourcesStringBuilder = new StringBuilder();

        private float _currentAmountResourcesScreenTime;
        private bool _amountResourcesInScreen;
        private int _currentAmountResources;

        private void Awake() => _rectTransform = GetComponent<RectTransform>();

        private void Start() => HUDFadeIn();

        private void Update() => ResourcesTimer();

        private void ResourcesTimer()
        {
            ResourcesInScreenTimer();
            AmountResourcesInScreenTimer();
        }

        private void AmountResourcesInScreenTimer()
        {
            if (_amountResourcesInScreen)
            {
                _currentAmountResourcesScreenTime += Time.deltaTime;
                if (_currentAmountResourcesScreenTime >= amountResourcesScreenTime)
                {
                    FadeCanvasGroup(amountResourcesCanvasGroup, 0f, amountResourcesFadeDuration);

                    _currentAmountResources = 0;
                    _currentAmountResourcesScreenTime = 0;
                    _amountResourcesInScreen = false;
                }
            }
        }

        private void ResourcesInScreenTimer()
        {
            if (_resourcesInScreen)
            {
                _currentResourcesScreenTime += Time.deltaTime;
                if (_currentResourcesScreenTime >= resourcesScreenTime)
                {
                    FadeCanvasGroup(resourcesCanvasGroup, 0f, resourcesFadeDuration);

                    _currentResourcesScreenTime = 0;
                    _resourcesInScreen = false;
                }
            }
        }

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

        private void ShakeHUD()
        {
            _rectTransform.DOShakeAnchorPos(shakeDuration, shakeStrength);
        }

        public void RecoveryHealthBar(float currentHealth, float maxHealth)
        {
            HealthBarTween(ref currentHealth, ref maxHealth, ref healthBarProgressive, ref healthBar);
        }

        public void DamageHealthBar(float currentHealth, float maxHealth)
        {
            HealthBarTween(ref currentHealth, ref maxHealth, ref healthBar, ref healthBarProgressive);
            ShakeHUD();
        }

        private void HealthBarTween(ref float currentHealth, ref float maxHealth, ref Image linearBar, ref Image progressiveBar)
        {
            float convertValue = currentHealth / maxHealth;

            linearBar.DOKill();
            linearBar.fillAmount = convertValue;

            progressiveBar.DOKill();
            progressiveBar.DOFillAmount(convertValue, progressiveBarDuration);
        }

        public void UltimateBar(ref float progress, float maxProgress)
        {
            float convertValue = progress / maxProgress;

            ultimateBar.fillAmount = convertValue;
            ultimateProgressive.fillAmount = convertValue;
        }

        public void UltimateBarReset()
        {
            ultimateBar.fillAmount = 0;

            ultimateProgressive.DOKill();
            ultimateProgressive.DOFillAmount(0, progressiveBarDuration);
        }

        public void UpdateResources(int amountResources, int currentResources)
        {
            if(_resourcesInScreen) { _currentResourcesScreenTime = 0; }

            resourcesTMP.text = currentResources.ToString();

            if(resourcesCanvasGroup.alpha < 1f)
            {
                FadeCanvasGroup(resourcesCanvasGroup, 1f, resourcesFadeDuration);
            }

            _currentAmountResources += amountResources;
            UpdateAmountResources();

            _resourcesInScreen = true;
        }

        private void FadeCanvasGroup(CanvasGroup canvas, float endValue, float duration)
        {
            canvas.DOKill();
            canvas.DOFade(endValue, duration);
        }

        private void UpdateAmountResources()
        {
            if(_amountResourcesInScreen) { _currentAmountResourcesScreenTime = 0; }

            _amountResourcesStringBuilder.Length = 0;

            _amountResourcesStringBuilder.Append("+");
            _amountResourcesStringBuilder.Append(_currentAmountResources);

            amountResourcesTMP.text = _amountResourcesStringBuilder.ToString();

            FadeCanvasGroup(amountResourcesCanvasGroup, 1f, amountResourcesFadeDuration);

            _amountResourcesInScreen = true;
        }
    }
}
