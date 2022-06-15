using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public class GameplayLoadTranslatedTextManager : LoadTranslatedTextManager
    {
        [Header("Gameplay Data")]
        [SerializeField] protected UITextMessageData[] gameplayMessageToLoad;

        protected override void LoadTranslation()
        {
            DefaultLoadTranslation();
        }

        protected void DefaultLoadTranslation()
        {
            if (LanguageLoaderManager.Instance == null) return;

            var languageLoader = LanguageLoaderManager.Instance;

            languageLoader.LoadGameplayMessageText(gameplayMessageToLoad);
        }
    }
}