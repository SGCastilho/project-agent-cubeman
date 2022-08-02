using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cubeman.UI
{
    public sealed class UIButtonLevelSelect : MonoBehaviour
    {
        public delegate void LoadSelectedStage(string stageName);
        public event LoadSelectedStage OnLoadSelectedStage;

        [Header("Classes")]
        [SerializeField] private Image stagePreview;
        [SerializeField] private TextMeshProUGUI tmpStageName;

        private string _stageToLoad;

        public void SetupButton(string stageKey, string stageName, Sprite stagePreviewSprite)
        {
            _stageToLoad = stageKey;

            tmpStageName.text = stageName;
            stagePreview.sprite = stagePreviewSprite;
        }

        public void SelectStage()
        {
            OnLoadSelectedStage?.Invoke(_stageToLoad);
        }
    }
}
