using Cubeman.UI;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class CutsceneEventsManager : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private SceneLoaderManager sceneLoaderManager;
        [SerializeField] private UIFade uiFade;

        [Space(6)]

        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private UIDialogue uiDialogue;

        private void OnEnable() => EnableSceneEvents();

        private void OnDisable() => DisableSceneEvents();

        private void EnableSceneEvents()
        {
            EnableSceneLoaderEvents();
            EnableDialogueEvents();
        }

        private void DisableSceneEvents()
        {
            DisableSceneLoaderEvents();
            DisableDialogueEvents();
        }

        private void EnableSceneLoaderEvents()
        {
            sceneLoaderManager.OnStartLoadScene += uiFade.LoadingFadeIn;
        }

        private void DisableSceneLoaderEvents()
        {
            sceneLoaderManager.OnStartLoadScene -= uiFade.LoadingFadeIn;
        }

        private void EnableDialogueEvents()
        {
            dialogueManager.OnDialogueReady += uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad += uiDialogue.SetDialogue;
            dialogueManager.OnDialogueComplete += uiDialogue.FadeOut;
        }

        private void DisableDialogueEvents()
        {
            dialogueManager.OnDialogueReady -= uiDialogue.FadeIn;
            dialogueManager.OnDialogueLoad -= uiDialogue.SetDialogue;
            dialogueManager.OnDialogueComplete -= uiDialogue.FadeOut;
        }
    }
}
