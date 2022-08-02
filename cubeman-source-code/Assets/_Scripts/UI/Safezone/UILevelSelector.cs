using DG.Tweening;
using Cubeman.ScriptableObjects;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Cubeman.UI
{
    public sealed class UILevelSelector : MonoBehaviour
    {
        private const string INSTANCE_NAME = "Group ";

        public delegate void SetupButtonsAction(UIButtonLevelSelect[] buttonsLevelSelect);
        public event SetupButtonsAction OnSetupButtonsAction;

        private StringBuilder stringBuilders = new StringBuilder();

        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        [Space(6)]

        [SerializeField] private RectTransform levelSelectPivot;
        [SerializeField] private GameObject levelSelectPrefab;

        [Space(6)]

        [SerializeField] private Button buttonMoveLeft;
        [SerializeField] private Button buttonMoveRight;

        [Header("Settings")]
        [SerializeField] private int spacingBetweenLevelSelect = 800;
        [SerializeField] [Range(0.1f, 1f)] private float movimentDurationBetweenLevelSelect = 1f;

        [Space(6)]

        [SerializeField] [Range(0.1f, 2f)] private float fadeDuration = 1f;

        private Image[] _stageSelectionButtons;

        private int _maxSelection;
        private int _currentSelection;
        private bool _inSelectionTransistion;

        public void CreateStages(ref StageLevelData[] stagesToCreate)
        {
            _stageSelectionButtons = new Image[stagesToCreate.Length];

            UIButtonLevelSelect[] stageButtons = new UIButtonLevelSelect[stagesToCreate.Length];

            var lastInstantiatePosistion = Vector2.zero;

            for (int i = 0; i < stagesToCreate.Length; i++)
            {
                var instance = Instantiate(levelSelectPrefab, levelSelectPivot);

                if (i > 0)
                {
                    var getAnchor = instance.GetComponent<RectTransform>();
                    getAnchor.anchoredPosition = new Vector2(lastInstantiatePosistion.x + spacingBetweenLevelSelect,
                        getAnchor.anchoredPosition.y);

                    lastInstantiatePosistion = getAnchor.anchoredPosition;
                }

                stringBuilders.Length = 0;

                stringBuilders.Append(INSTANCE_NAME);
                stringBuilders.Append(stagesToCreate[i].Name);

                instance.name = stringBuilders.ToString();

                var setupButton = instance.GetComponent<UIButtonLevelSelect>();

                setupButton.SetupButton(stagesToCreate[i].Key, stagesToCreate[i].Name, stagesToCreate[i].Preview);

                stageButtons[i] = setupButton;

                _stageSelectionButtons[i] = instance.GetComponentInChildren<Image>();
                _stageSelectionButtons[i].raycastTarget = false;
            }

            _maxSelection = stagesToCreate.Length - 1;

            SetupStartSelection();

            OnSetupButtonsAction?.Invoke(stageButtons);
        }

        private void SetupStartSelection()
        {
            _currentSelection = _maxSelection;

            var startSelection = new Vector2(-(spacingBetweenLevelSelect * _currentSelection),
                levelSelectPivot.anchoredPosition.y);

            levelSelectPivot.anchoredPosition = startSelection;

            buttonMoveRight.interactable = false;

            _stageSelectionButtons[_currentSelection].raycastTarget = true;
        }

        public void FadeIn()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, fadeDuration).OnComplete(EnableInteraction);
        }

        public void FadeOut()
        {
            DisableInteraction();

            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, fadeDuration);
        }

        public void MoveSelectionRight()
        {
            if (_currentSelection >= _maxSelection || _inSelectionTransistion) return;

            _inSelectionTransistion = true;

            DisableInteraction();

            var movimentX = levelSelectPivot.anchoredPosition.x - spacingBetweenLevelSelect;

            levelSelectPivot.DOKill();
            levelSelectPivot.DOAnchorPosX(movimentX, movimentDurationBetweenLevelSelect)
                .OnComplete(EndSelectionTransistion);

            _stageSelectionButtons[_currentSelection].raycastTarget = false;

            _currentSelection++;

            _stageSelectionButtons[_currentSelection].raycastTarget = true;

            if (_currentSelection >= _maxSelection)
            {
                buttonMoveRight.interactable = false;
            }

            if (_currentSelection > 0)
            {
                buttonMoveLeft.interactable = true;
            }
        }

        public void MoveSelectionLeft()
        {
            if (_currentSelection <= 0 || _inSelectionTransistion) return;

            _inSelectionTransistion = true;

            DisableInteraction();

            var movimentX = levelSelectPivot.anchoredPosition.x + spacingBetweenLevelSelect;

            levelSelectPivot.DOKill();
            levelSelectPivot.DOAnchorPosX(movimentX, movimentDurationBetweenLevelSelect)
                .OnComplete(EndSelectionTransistion);

            _stageSelectionButtons[_currentSelection].raycastTarget = false;

            _currentSelection--;

            _stageSelectionButtons[_currentSelection].raycastTarget = true;

            if (_currentSelection < _maxSelection)
            {
                buttonMoveRight.interactable = true;
            }

            if (_currentSelection <= 0)
            {
                buttonMoveLeft.interactable = false;
            }
        }

        private void EndSelectionTransistion()
        {
            _inSelectionTransistion = false;

            EnableInteraction();
        }

        private void EnableInteraction()
        {
            canvasGroup.blocksRaycasts = true;
        }

        private void DisableInteraction()
        {
            canvasGroup.blocksRaycasts = false;
        }
    }
}
