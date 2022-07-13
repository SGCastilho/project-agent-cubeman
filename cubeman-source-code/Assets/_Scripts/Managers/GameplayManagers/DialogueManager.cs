using System;
using System.Collections;
using Cubeman.Player;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class DialogueManager : MonoBehaviour
    {
        public delegate void DialogueLoad(ref Sprite face, ref string name, ref string dialogue);
        public event DialogueLoad OnDialogueLoad;

        public Action OnDialogueReady;
        public Action OnDialogueComplete;

        public Action OnDialogueFinish;
    
        private DialogueMessageData _dialogueData;

        private PlayerBehaviour _player;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float moveToNextDialogueDuration = 1f;
        private bool _canMoveToNextDialogue;

        private int _maxDialogueID;
        private int _currentDialogueID;
        private Sprite _currentDialogueCharacterSprite;
        private string _currentDialogueCharacterName;
        private string _currentDialogueText;

        private void Awake() => SetupManager();

        private void SetupManager()
        {
            _player = FindObjectOfType<PlayerBehaviour>();
        }

        public void StartDialogue(DialogueMessageData dialogue)
        {
            if(GamePauseManager.Instance != null)
            {
                GamePauseManager.Instance.BlockPause = true;
            }

            _dialogueData = dialogue;
            _maxDialogueID = _dialogueData.DialogueSequence.Length;

            PrepareUIInputs();

            LoadDialogueData();

            OnDialogueReady?.Invoke();

            StartCoroutine(NextDialogueCoroutine());
        }

        private void PrepareUIInputs()
        {
            _player.Input.GameplayInputs(false);
            _player.Input.SubscribeSubmitInput(NextDialogue);
            _player.Input.UIInputs(true);
        }

        public void NextDialogue()
        {
            if(!_canMoveToNextDialogue) return;

            _currentDialogueID++;

            if(_currentDialogueID < _maxDialogueID)
            {
                LoadDialogueData();
                
                OnDialogueReady?.Invoke();

                StartCoroutine(NextDialogueCoroutine());
            }
            else
            {
                if (GamePauseManager.Instance != null)
                {
                    GamePauseManager.Instance.BlockPause = false;
                }

                PrepareGameplayInput();

                OnDialogueComplete?.Invoke();
                OnDialogueFinish?.Invoke();

                ClearDialogueData();
            }
        }

        private void PrepareGameplayInput()
        {
            _player.Input.UIInputs(false);
            _player.Input.UnSubscribeSubmitInput();
            _player.Input.GameplayInputs(true);
        }

        IEnumerator NextDialogueCoroutine()
        {
            _canMoveToNextDialogue = false;

            yield return new WaitForSeconds(moveToNextDialogueDuration);

            _canMoveToNextDialogue = true;
        }

        private void LoadDialogueData()
        {
            _currentDialogueCharacterName = _dialogueData.DialogueSequence[_currentDialogueID].CharacterName;
            _currentDialogueCharacterSprite = _dialogueData.DialogueSequence[_currentDialogueID].CharacterSprite;
            _currentDialogueText = _dialogueData.DialogueSequence[_currentDialogueID].CharacterDialogue;

            OnDialogueLoad?.Invoke(ref _currentDialogueCharacterSprite, ref _currentDialogueCharacterName, ref _currentDialogueText);
        }

        private void ClearDialogueData()
        {
            _currentDialogueID = 0;

            _canMoveToNextDialogue = false;

            _currentDialogueText = string.Empty;
            _currentDialogueCharacterName = string.Empty;

            _currentDialogueCharacterSprite = null;
        }
    }
}
