using System.Threading.Tasks;
using System.Collections.Generic;
using Cubeman.Utilities;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class LanguageLoaderManager : MonoBehaviour
    {
        #region Singleton
        public static LanguageLoaderManager Instance;
        #endregion

        private CSVReader _uiMainMenuMessageReader;
        private CSVReader _uiOptionsMenuMessageReader;

        private CSVReader _uiGameplayMessageReader;
        private CSVReader _uiScreenMessageReader;
        private CSVReader _uiDialogueMessageReader;

        private TextAsset _uiMainMenuMessageCSV;
        private TextAsset _uiOptionsMenuMessageCSV;

        private TextAsset _uiGameplayMessageCSV;
        private TextAsset _uiScreenMessageCSV;
        private TextAsset _uiDialogueMessageCSV;

        private Dictionary<string, string[]> _uiMainMenuMessageTranslations;
        private Dictionary<string, string[]> _uiOptionsMenuMessageTranslations;

        private Dictionary<string, string[]> _uiGameplayMessageTranslations;
        private Dictionary<string, string[]> _uiScreenMessageTranslations;
        private Dictionary<string, string[]> _uiDialogueTranslations;

        private const int COLUMS_IGNORE = 3;

        private int _clientLanguageIndex;

        [SerializeField] private string[] teste;

        private void Start()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public async Task LoadTranslations()
        {
            await LoadCSV();
            await CheckSystemLanguage();
            await LoadCSVTranslations();
        }

        private async Task LoadCSV()
        {
            _uiMainMenuMessageCSV = Resources.Load<TextAsset>("CSV/Translation/UI/csv_translation_mainMenu");
            _uiOptionsMenuMessageCSV = Resources.Load<TextAsset>("CSV/Translation/UI/csv_translation_optionsMenu");

            _uiGameplayMessageCSV = Resources.Load<TextAsset>("CSV/Translation/UI/csv_translation_gameplay");
            _uiScreenMessageCSV = Resources.Load<TextAsset>("CSV/Translation/UI/csv_translation_screen_message");
            _uiDialogueMessageCSV = Resources.Load<TextAsset>("CSV/Translation/UI/csv_translation_dialogue");

            _uiMainMenuMessageReader = new CSVReader(_uiMainMenuMessageCSV);
            _uiOptionsMenuMessageReader = new CSVReader(_uiOptionsMenuMessageCSV);

            _uiGameplayMessageReader = new CSVReader(_uiGameplayMessageCSV);
            _uiScreenMessageReader = new CSVReader(_uiScreenMessageCSV);
            _uiDialogueMessageReader = new CSVReader(_uiDialogueMessageCSV);

            await Task.Yield();
        }

        private async Task CheckSystemLanguage()
        {
            var clientLanguage = Application.systemLanguage;

            switch (clientLanguage)
            {
                case SystemLanguage.English:
                    _clientLanguageIndex = 0;
                    break;
                case SystemLanguage.Portuguese:
                    _clientLanguageIndex = 1;
                    break;
                default:
                    _clientLanguageIndex = 0;
                    break;
            }

            await Task.Yield();
        }

        private async Task LoadCSVTranslations()
        {
            await LoadUIMainMenuMessageCSV();
            await LoadUIOptionsMenuMessageCSV();

            await LoadGameplayMessageCSV();
            await LoadScreenMessageCSV();
            await LoadDialogueMessageCSV();

            await Task.Yield();
        }

        private async Task LoadScreenMessageCSV()
        {
            var translationData = _uiScreenMessageReader.Read();

            _uiScreenMessageTranslations = new Dictionary<string, string[]>();

            for (int i = COLUMS_IGNORE; i < translationData.Length; i += COLUMS_IGNORE)
            {
                var textKey = translationData[i];

                string[] textLanguage = new string[2];
                textLanguage[0] = translationData[i + 1];
                textLanguage[1] = translationData[i + 2];

                _uiScreenMessageTranslations.Add(textKey, textLanguage);
            }

            await Task.Yield();
        }

        private async Task LoadUIMainMenuMessageCSV()
        {
            var translationData = _uiMainMenuMessageReader.Read();

            _uiMainMenuMessageTranslations = new Dictionary<string, string[]>();

            for (int i = COLUMS_IGNORE; i < translationData.Length; i += COLUMS_IGNORE)
            {
                var textKey = translationData[i];

                string[] textLanguage = new string[2];
                textLanguage[0] = translationData[i + 1];
                textLanguage[1] = translationData[i + 2];

                _uiMainMenuMessageTranslations.Add(textKey, textLanguage);
            }

            await Task.Yield();
        }

        private async Task LoadUIOptionsMenuMessageCSV()
        {
            var translationData = _uiOptionsMenuMessageReader.Read();

            _uiOptionsMenuMessageTranslations = new Dictionary<string, string[]>();

            for (int i = COLUMS_IGNORE; i < translationData.Length; i += COLUMS_IGNORE)
            {
                var textKey = translationData[i];

                string[] textLanguage = new string[2];
                textLanguage[0] = translationData[i + 1];
                textLanguage[1] = translationData[i + 2];

                _uiOptionsMenuMessageTranslations.Add(textKey, textLanguage);
            }

            await Task.Yield();
        }

        private async Task LoadGameplayMessageCSV()
        {
            var translationData = _uiGameplayMessageReader.Read();

            _uiGameplayMessageTranslations = new Dictionary<string, string[]>();

            for (int i = COLUMS_IGNORE; i < translationData.Length; i += COLUMS_IGNORE)
            {
                var textKey = translationData[i];

                string[] textLanguage = new string[2];
                textLanguage[0] = translationData[i + 1];
                textLanguage[1] = translationData[i + 2];

                _uiGameplayMessageTranslations.Add(textKey, textLanguage);
            }

            await Task.Yield();
        }

        private async Task LoadDialogueMessageCSV()
        {
            teste = _uiDialogueMessageReader.Read();
            var translationData = _uiDialogueMessageReader.Read();

            var dialoguesData = Resources.LoadAll<DialogueMessageData>("ScriptableObjects/Dialogues");

            _uiDialogueTranslations = new Dictionary<string, string[]>();

            for (int i = 0; i < dialoguesData.Length; i++)
            {
                var dialogueKey = dialoguesData[i].Key;

                int lastKeyIndex = 0;

                for(int j = COLUMS_IGNORE; j < translationData.Length; j++)
                {
                    if(translationData[j] == dialogueKey)
                    {
                        lastKeyIndex = j;
                        break;
                    }
                }

                var dialogueTextLeagth = (dialoguesData[i].DialogueSequence.Length * 2) + (lastKeyIndex + 1);

                string[] textLanguage = new string[dialoguesData[i].DialogueSequence.Length * 2];

                for(int j = lastKeyIndex; j < dialogueTextLeagth; j++)
                {
                    try 
                    {
                        if (translationData[j] != dialogueKey)
                        {
                            var adjustIndex = j - (lastKeyIndex + 1);
                            textLanguage[adjustIndex] = translationData[j];
                        }
                    }
                    catch
                    {
                        Debug.LogWarning($"Não foi possivel achar a tradução, index{j}");
                    }
                }

                _uiDialogueTranslations.Add(dialogueKey, textLanguage);
            }

            await Task.Yield();
        }

        public void LoadUIMainMenuMessageText(UITextMessageData[] messageToLoad)
        {
            for (int i = 0; i < messageToLoad.Length; i++)
            {
                if (_uiMainMenuMessageTranslations.ContainsKey(messageToLoad[i].Key))
                {
                    var textKey = messageToLoad[i].Key;
                    messageToLoad[i].SetMessage(_uiMainMenuMessageTranslations[textKey][_clientLanguageIndex]);
                }
            }
        }

        public void LoadUIOptionsMenuMessageText(UITextMessageData[] messageToLoad)
        {
            for (int i = 0; i < messageToLoad.Length; i++)
            {
                if (_uiOptionsMenuMessageTranslations.ContainsKey(messageToLoad[i].Key))
                {
                    var textKey = messageToLoad[i].Key;
                    messageToLoad[i].SetMessage(_uiOptionsMenuMessageTranslations[textKey][_clientLanguageIndex]);
                }
            }
        }

        public void LoadScreenMessageText(ScreenMessageData[] messageToLoad)
        {
            for (int i = 0; i < messageToLoad.Length; i++)
            {
                if (_uiScreenMessageTranslations.ContainsKey(messageToLoad[i].Key))
                {
                    var textKey = messageToLoad[i].Key;
                    messageToLoad[i].SetupText(_uiScreenMessageTranslations[textKey][_clientLanguageIndex]);
                }
            }
        }

        public void LoadGameplayMessageText(UITextMessageData[] messageToLoad)
        {
            for (int i = 0; i < messageToLoad.Length; i++)
            {
                if (_uiGameplayMessageTranslations.ContainsKey(messageToLoad[i].Key))
                {
                    var textKey = messageToLoad[i].Key;
                    messageToLoad[i].SetMessage(_uiGameplayMessageTranslations[textKey][_clientLanguageIndex]);
                }
            }
        }

        public void LoadDialogueText(DialogueMessageData[] messageToLoad)
        {
            for (int i = 0; i < messageToLoad.Length; i++)
            {
                if (_uiDialogueTranslations.ContainsKey(messageToLoad[i].Key))
                {
                    var textKey = messageToLoad[i].Key;

                    List<string> translatedDialogue = new List<string>();

                    for (int j = _clientLanguageIndex; j < _uiDialogueTranslations[textKey].Length; j += 2)
                    {
                        translatedDialogue.Add(_uiDialogueTranslations[textKey][j]);
                    }

                    var dialogues = new string[messageToLoad[i].DialogueSequence.Length];

                    for(int j = 0; j < translatedDialogue.Count; j++)
                    {
                        dialogues[j] = translatedDialogue[j];
                    }

                    messageToLoad[i].SetupDialoguesText(dialogues);
                }
            }
        }
    }
}