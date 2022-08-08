using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class SafezoneInteractionsManager : MonoBehaviour
    {
        public delegate void ShowInteractionsWindow();
        public event ShowInteractionsWindow OnShowInteractionsWindow;

        public delegate void HideInteractionWindow();
        public event HideInteractionWindow OnHideInteractionWindow;

        [Header("Dialogues Data")]
        [SerializeField] private DialogueMessageData[] oracleDialogues;
        [SerializeField] private DialogueMessageData[] gunsmithDialogues;
        [SerializeField] private DialogueMessageData[] scarlettDialogues;

        [Header("Classes")]
        [SerializeField] private DialogueManager dialogueManager;

        private int _currentSafezoneState;

        private void Awake() => SetupTalkDialogues();

        private void SetupTalkDialogues()
        {
            _currentSafezoneState = (int)SafezoneScenarioTriggersManager.Instance.CurrentState;
        }

        public void ShowInteractions()
        {
            if (GamePauseManager.Instance.GamePaused) return;

            GamePauseManager.Instance.BlockPause = true;

            OnShowInteractionsWindow?.Invoke();
        }

        public void HideInteractions()
        {
            OnHideInteractionWindow?.Invoke();

            GamePauseManager.Instance.BlockPause = false;
        }

        public void StartOracleDialogue()
        {
            HideInteractions();

            dialogueManager.StartDialogue(oracleDialogues[_currentSafezoneState]);
        }

        public void StartGunsmithDialogue()
        {
            HideInteractions();

            dialogueManager.StartDialogue(gunsmithDialogues[_currentSafezoneState]);
        }

        public void StartScarlettDialogue()
        {
            HideInteractions();

            dialogueManager.StartDialogue(scarlettDialogues[_currentSafezoneState]);
        }
    }
}
