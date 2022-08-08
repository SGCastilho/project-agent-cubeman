using UnityEngine;

namespace Cubeman.Manager
{
    public enum SafezoneState 
    {
        LEVEL_1_COMPLETED,
        LEVEL_2_COMPLETED,
        LEVEL_3_COMPLETED,
        LEVEL_4_COMPLETED,
        LEVEL_5_COMPLETED
    }

    public sealed class SafezoneScenarioTriggersManager : MonoBehaviour
    {
        #region Singleton
        public static SafezoneScenarioTriggersManager Instance { get; private set; }
        #endregion

        #region Encapsulation
        public SafezoneState CurrentState { get => _currentSafeZoneState; }
        #endregion

        private SafezoneState _currentSafeZoneState;

        private void Awake() => SingletonInstance();

        private void SingletonInstance()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetSafezoneState(SafezoneState newState)
        {
            _currentSafeZoneState = newState;
        }

        public void DestroySafezoneState()
        {
            Destroy(Instance.gameObject);
        }
    }
}
