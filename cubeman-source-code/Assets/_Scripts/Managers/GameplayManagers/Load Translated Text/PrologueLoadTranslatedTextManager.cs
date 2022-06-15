using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class PrologueLoadTranslatedTextManager : GameplayLoadTranslatedTextManager
    {
        [Header("Exclusive Settings")]
        [SerializeField] private ScreenMessageData[] screenMessageToLoad;
        [SerializeField] private DialogueMessageData[] dialogueMessageToLoad;

        protected override void LoadTranslation()
        {
            base.LoadTranslation();

            if (LanguageLoaderManager.Instance == null) return;

            var languageLoader = LanguageLoaderManager.Instance;

            languageLoader.LoadDialogueText(dialogueMessageToLoad);
            languageLoader.LoadScreenMessageText(screenMessageToLoad);
        }
    }
}