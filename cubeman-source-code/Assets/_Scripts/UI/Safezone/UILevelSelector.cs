using DG.Tweening;
using Cubeman.ScriptableObjects;
using System.Text;
using UnityEngine;

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

        [SerializeField] private Transform levelSelectPivot;
        [SerializeField] private GameObject levelSelectPrefab;

        [Header("Settings")]
        [SerializeField] private int spacingBetweenLevelSelect = 800;

        [Space(6)]

        [SerializeField] [Range(0.1f, 2f)] private float fadeDuration = 1f;

        public void CreateStages(ref StageLevelData[] stagesToCreate)
        {
            UIButtonLevelSelect[] stageButtons = new UIButtonLevelSelect[stagesToCreate.Length];

            var lastInstantiatePosistion = Vector2.zero;

            for(int i = 0; i < stagesToCreate.Length; i++)
            {
                var instance = Instantiate(levelSelectPrefab, levelSelectPivot);

                if(i > 0)
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
            }

            OnSetupButtonsAction?.Invoke(stageButtons);
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
