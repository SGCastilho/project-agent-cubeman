using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cubeman.UI
{
    public sealed class UISafezoneInteractions : MonoBehaviour
    {
        public delegate void SwitchingInputs(bool switchInputs);
        public event SwitchingInputs OnSwitchingInputs;

        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private EventSystem scenarioEventSystem;

        [Space(6)]

        [SerializeField] private GameObject upgradeZoneButtons;
        [SerializeField] private GameObject levelSelectorButtons;

        [Header("Settings")]
        [SerializeField] [Range(0.1f , 1f)] private float fadeDuration = 0.4f;

        private bool _inFadeTransistion;

        public void ShowInteractions()
        {
            if (_inFadeTransistion) return;

            FadeIn();

            OnSwitchingInputs?.Invoke(true);
        }

        public void HideInteractions()
        {
            if (_inFadeTransistion) return;

            scenarioEventSystem.SetSelectedGameObject(null);

            FadeOut();

            OnSwitchingInputs?.Invoke(false);
        }

        public void HideInteractionsWithoutSwithInputs()
        {
            if (_inFadeTransistion) return;

            scenarioEventSystem.SetSelectedGameObject(null);

            FadeOut();
        }

        public void SetupToLevelSelector()
        {
            upgradeZoneButtons.SetActive(false);
            levelSelectorButtons.SetActive(true);
        }

        public void SetupToUpgradeZone()
        {
            upgradeZoneButtons.SetActive(true);
            levelSelectorButtons.SetActive(false);
        }

        private void FadeIn()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration).OnComplete(EnableInteraction);

            _inFadeTransistion = true;
        }

        private void FadeOut()
        {
            DisableInteraction();

            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(ResetButtonsGroup);

            _inFadeTransistion = true;
        }

        private void EnableInteraction()
        {
            _inFadeTransistion = false;
            canvasGroup.blocksRaycasts = true;
        }

        private void DisableInteraction()
        {
            canvasGroup.blocksRaycasts = false;
        }

        private void ResetButtonsGroup()
        {
            upgradeZoneButtons.SetActive(false);
            levelSelectorButtons.SetActive(false);

            _inFadeTransistion = false;
        }
    }
}
