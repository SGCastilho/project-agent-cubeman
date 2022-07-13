using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public class CutsceneLoadTranslatedTextManager : LoadTranslatedTextManager
    {
        [Header("Cutscene Data")]
        [SerializeField] protected DialogueMessageData[] dialoguesMessagesToLoad;

        protected override void LoadTranslation()
        {
            DefaultLoadTranslation();
        }

        protected void DefaultLoadTranslation()
        {
            if (LanguageLoaderManager.Instance == null) return;

            var languageLoader = LanguageLoaderManager.Instance;

            languageLoader.LoadDialogueText(dialoguesMessagesToLoad);
        }
    }
}