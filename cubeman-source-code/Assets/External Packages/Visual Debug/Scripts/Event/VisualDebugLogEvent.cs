using Core.UI;
using Core.Manager;
using UnityEngine;

namespace Core.Event
{
    public sealed class VisualDebugLogEvent : MonoBehaviour
    {
        private static VisualDebugLogEvent Singleton;

        [Header("Classes")]
        [SerializeField] private UI_VisualDebug uiVisualDebug;

        private void Awake() => SetupEvent();

        private void OnDestroy() => DisableEvents();

        private void SetupEvent()
        {
            EnableEvents();
            SetupSingleton();
        }

        private void SetupSingleton()
        {
            if (Singleton == null)
            {
                Singleton = this;
                DontDestroyOnLoad(this);
            }
            else { Destroy(gameObject); }
        }

        private void EnableEvents()
        {
            VisualDebug.OnLogMessage += uiVisualDebug.LogMessage;
            VisualDebug.OnLogWarningMessage += uiVisualDebug.LogWarningMessage;
            VisualDebug.OnLogErrorMessage += uiVisualDebug.LogErrorMessage;

            VisualDebug.OnLogMessageWithSeconds += uiVisualDebug.LogMessage;
            VisualDebug.OnLogWarningMessageWithSeconds += uiVisualDebug.LogWarningMessage;
            VisualDebug.OnLogErrorMessageWithSeconds += uiVisualDebug.LogErrorMessage;
        }

        private void DisableEvents()
        {
            VisualDebug.OnLogMessage -= uiVisualDebug.LogMessage;
            VisualDebug.OnLogWarningMessage -= uiVisualDebug.LogWarningMessage;
            VisualDebug.OnLogErrorMessage -= uiVisualDebug.LogErrorMessage;

            VisualDebug.OnLogMessageWithSeconds -= uiVisualDebug.LogMessage;
            VisualDebug.OnLogWarningMessageWithSeconds -= uiVisualDebug.LogWarningMessage;
            VisualDebug.OnLogErrorMessageWithSeconds -= uiVisualDebug.LogErrorMessage;
        }
    }
}
