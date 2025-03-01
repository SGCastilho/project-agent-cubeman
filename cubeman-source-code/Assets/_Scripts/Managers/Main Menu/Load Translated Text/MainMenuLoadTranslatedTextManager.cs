using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class MainMenuLoadTranslatedTextManager : LoadTranslatedTextManager
    {
        [Header("Data")]
        [SerializeField] private UITextMessageData[] uiMainMenuTextToLoad;
        [SerializeField] private UITextMessageData[] uiOptionsMenuTextToLoad;

        protected override void LoadTranslation()
        {
            if (LanguageLoaderManager.Instance == null) return;

            var languageLoader = LanguageLoaderManager.Instance;

            languageLoader.LoadUIMainMenuMessageText(uiMainMenuTextToLoad);
            languageLoader.LoadUIOptionsMenuMessageText(uiOptionsMenuTextToLoad);
        }
    }
}