using DG.Tweening;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIMenuGroupWelcome : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private CanvasGroup logoCanvasGroup;
        [SerializeField] private CanvasGroup pressAnyKeyCanvasGroup;

        private RectTransform _logoRectTransform;
        private RectTransform _pressAnyKeyTransform;
        private UIPressAnyKeyLoop _uiPressAnyKeyLoop;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 2f)] private float logoCanvasFadeInDuration = 1f;
        [SerializeField] [Range(0.1f, 2f)] private float pressAnyKeyFadeInDuration = 1f;

        [Space(6)]

        [SerializeField] [Range(0.1f, 2f)] private float logoCanvasFadeOutDuration = 1f;

        [Space(6)]

        [SerializeField] [Range(1f, 4f)] private float logoDelay = 2f;
        [SerializeField] [Range(1f, 4f)] private float pressAnyKeyDelay = 2f;

        [Space(12)]

        [SerializeField] private float logoMovimentY = 140;
        [SerializeField] private float pressAnyKeyMovimentY = 140;

        [Space(12)]

        [SerializeField] private Vector3 welcomeEndPressAnyKeyScale;

        [Space(6)]

        [SerializeField] [Range(0.1f, 0.4f)] private float welcomeEndScalePressAnyKeyInDuration = 0.4f;
        [SerializeField] [Range(1f, 4f)] private float welcomeEndScalePressAnyKeyOutDuration = 2f;

        private void Awake() => CacheComponents();

        private void CacheComponents()
        {
            _logoRectTransform = logoCanvasGroup.GetComponent<RectTransform>();
            _pressAnyKeyTransform = pressAnyKeyCanvasGroup.GetComponent<RectTransform>();

            _uiPressAnyKeyLoop = pressAnyKeyCanvasGroup.GetComponent<UIPressAnyKeyLoop>();
        }

        private void Start() => WelcomeStartTween();

        private void WelcomeStartTween()
        {
            FadeIn(logoCanvasGroup, logoCanvasFadeInDuration, logoDelay);
            MoveY(_logoRectTransform, logoMovimentY, logoCanvasFadeInDuration, logoDelay);

            FadeIn(pressAnyKeyCanvasGroup, pressAnyKeyFadeInDuration, pressAnyKeyDelay);
            MoveY(_pressAnyKeyTransform, pressAnyKeyMovimentY, pressAnyKeyFadeInDuration, pressAnyKeyDelay, 
                _uiPressAnyKeyLoop.StartPressAnyKeyLoop);
        }

        //CHAMADO QUANDO APERTAR A TECLA
        public void WelcomeEndTween()
        {
            _uiPressAnyKeyLoop.PressedAnyKey();

            _pressAnyKeyTransform.DOScale(welcomeEndPressAnyKeyScale, welcomeEndScalePressAnyKeyInDuration).
                OnComplete(WelcomeScaleEnd);
        }

        private void WelcomeScaleEnd()
        {
            var startScale = new Vector3(1, 1, 1);
            _pressAnyKeyTransform.DOScale(startScale, welcomeEndScalePressAnyKeyOutDuration);

            FadeOut(pressAnyKeyCanvasGroup, welcomeEndScalePressAnyKeyOutDuration, GoToMainMenuTween);
        }

        private void GoToMainMenuTween()
        {
            FadeOut(logoCanvasGroup, logoCanvasFadeOutDuration);
        }

        private void FadeIn(CanvasGroup canvasGroup, float fadeDuration)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration);
        }

        private void FadeIn(CanvasGroup canvasGroup, float fadeDuration, TweenCallback action)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration).OnComplete(action);
        }

        private void FadeIn(CanvasGroup canvasGroup, float fadeDuration, float delay)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration).SetDelay(delay);
        }

        private void FadeOut(CanvasGroup canvasGroup, float fadeDuration)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration);
        }

        private void FadeOut(CanvasGroup canvasGroup, float fadeDuration, TweenCallback action)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(action);
        }

        private void FadeOut(CanvasGroup canvasGroup, float fadeDuration, float delay)
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration).SetDelay(delay);
        }

        private void MoveY(RectTransform rectTransform, float movimentAmount,float duration, float delay)
        {
            rectTransform.DOKill();
            rectTransform.DOAnchorPosY(movimentAmount, duration).SetDelay(delay);
        }

        private void MoveY(RectTransform rectTransform, float movimentAmount, float duration, float delay, TweenCallback onComplete)
        {
            rectTransform.DOKill();
            rectTransform.DOAnchorPosY(movimentAmount, duration).SetDelay(delay).OnComplete(onComplete);
        }
    }
}
