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

        private CSVReader _screenMessageReader;
        private CSVReader _dialogueMessageReader;

        private TextAsset _screenMessageCSV;
        private TextAsset _dialogueMessageCSV;

        private Dictionary<string, string[]> _dialogueTranslations;
        private Dictionary<string, string[]> _screenMessageTranslations;

        private const int COLUMS_IGNORE = 3;

        private int _clientLanguageIndex;

        //DEBUG VARIABLE
        [SerializeField] private string[] dataT;

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
            _screenMessageCSV = Resources.Load<TextAsset>("CSV/Translation/csv_translation_screen_message");
            _dialogueMessageCSV = Resources.Load<TextAsset>("CSV/Translation/csv_translation_dialogue");

            _screenMessageReader = new CSVReader(_screenMessageCSV);
            _dialogueMessageReader = new CSVReader(_dialogueMessageCSV);

            dataT = _dialogueMessageReader.Read();

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
            await LoadScreenMessageCSV();
            await LoadDialogueMessageCSV();

            await Task.Yield();
        }

        private async Task LoadScreenMessageCSV()
        {
            var translationData = _screenMessageReader.Read();

            _screenMessageTranslations = new Dictionary<string, string[]>();

            for (int i = COLUMS_IGNORE; i < translationData.Length; i += COLUMS_IGNORE)
            {
                var textKey = translationData[i];

                string[] textLanguage = new string[2];
                textLanguage[0] = translationData[i + 1];
                textLanguage[1] = translationData[i + 2];

                _screenMessageTranslations.Add(textKey, textLanguage);
            }

            await Task.Yield();
        }

        private async Task LoadDialogueMessageCSV()
        {
            var translationData = _dialogueMessageReader.Read();

            var dialoguesData = Resources.LoadAll<DialogueMessageData>("ScriptableObjects/Dialogues/Tutorial");

            _dialogueTranslations = new Dictionary<string, string[]>();

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
                    if (translationData[j] != dialogueKey)
                    {
                        var adjustIndex = j - (lastKeyIndex + 1);
                        textLanguage[adjustIndex] = translationData[j];
                    }
                }

                _dialogueTranslations.Add(dialogueKey, textLanguage);
            }

            await Task.Yield();
        }

        public void LoadScreenMessageText(ScreenMessageData[] messageToLoad)
        {
            for (int i = 0; i < messageToLoad.Length; i++)
            {
                if (_screenMessageTranslations.ContainsKey(messageToLoad[i].Key))
                {
                    var textKey = messageToLoad[i].Key;
                    messageToLoad[i].SetupText(_screenMessageTranslations[textKey][_clientLanguageIndex]);
                }
            }
        }

        public void LoadDialogueText(DialogueMessageData[] messageToLoad)
        {
            for (int i = 0; i < messageToLoad.Length; i++)
            {
                if (_dialogueTranslations.ContainsKey(messageToLoad[i].Key))
                {
                    var textKey = messageToLoad[i].Key;

                    List<string> translatedDialogue = new List<string>();

                    for (int j = _clientLanguageIndex; j < _dialogueTranslations[textKey].Length; j += 2)
                    {
                        translatedDialogue.Add(_dialogueTranslations[textKey][j]);
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