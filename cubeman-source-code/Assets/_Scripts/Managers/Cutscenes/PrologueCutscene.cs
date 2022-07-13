using System.Collections;
using Cubeman.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Manager
{
    public sealed class PrologueCutscene : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private DialogueManager dialogueManager;

        [Header("Settings")]
        [SerializeField] private DialogueMessageData[] dialogues;

        [Space(12)]

        [SerializeField] [Range(1f, 12f)] private float firstDialogueDelay = 4f;
        [SerializeField] [Range(1f, 12f)] private float delayBetweenDialogues = 2f;

        [Space(12)]

        [SerializeField] private UnityEvent OnCutsceneEnd;

        private int _currentDialogue;

        private void Start() => StartCoroutine(DialogueDelayCoroutine(firstDialogueDelay));

        private void DialogueFinish()
        {
            dialogueManager.OnDialogueFinish -= DialogueFinish;

            _currentDialogue++;

            StartCoroutine(DialogueDelayCoroutine(delayBetweenDialogues));
        }

        private void CutsceneEnd()
        {
            OnCutsceneEnd?.Invoke();
        }

        IEnumerator DialogueDelayCoroutine(float delayDuration)
        {
            yield return new WaitForSeconds(delayDuration);

            if(_currentDialogue < dialogues.Length)
            {
                dialogueManager.StartDialogue(dialogues[_currentDialogue]);
                dialogueManager.OnDialogueFinish += DialogueFinish;
            }
            else 
            {
                CutsceneEnd();
            }
        }
    }
}
