using Cubeman.Player;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class LevelSelectorManager : MonoBehaviour
    {
        public delegate void CreateStages(ref StageLevelData[] stagesToCreate);
        public event CreateStages OnCreateStages;

        public delegate void ShowLevelSelector();
        public event ShowLevelSelector OnShowLevelSelector;

        public delegate void HideLevelSelector();
        public event HideLevelSelector OnHideLevelSelector;

        [Header("Stage Data")]
        [SerializeField] private StageLevelData[] stagesToSelect;

        private PlayerBehaviour _playerBehaviour;

        private void Awake() => SetupManager();

        private void SetupManager()
        {
            CacheComponents();
            SetupStages();
        }

        private void SetupStages()
        {
            OnCreateStages?.Invoke(ref stagesToSelect);
        }
        
        private void CacheComponents()
        {
            _playerBehaviour = PlayerBehaviour.Instance;
        }

        public void OpenLevelSelector()
        {
            _playerBehaviour.Input.SubscribeCancelInput(CloseLevelSelector);

            _playerBehaviour.Input.UIInputs(true);
            _playerBehaviour.Input.GameplayInputs(false);

            _playerBehaviour.CursorState(false);

            OnShowLevelSelector?.Invoke();
        }

        public void CloseLevelSelector()
        {
            OnHideLevelSelector?.Invoke();

            _playerBehaviour.Input.UnSubscribeCancelInput();

            _playerBehaviour.Input.UIInputs(false);
            _playerBehaviour.Input.GameplayInputs(true);

            _playerBehaviour.CursorState(true);
        }
    }
}