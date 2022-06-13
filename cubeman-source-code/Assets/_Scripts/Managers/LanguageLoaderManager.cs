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
        private TextAsset _screenMessageCSV;

        private Dictionary<string, string[]> _screenMessageTranslations;
        private ScreenMessageData[] _screenMessageData;

        private const int COLUMS_IGNORE = 3;

        private int _clientLanguageIndex;

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
            _screenMessageReader = new CSVReader(_screenMessageCSV);

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

                if (i + COLUMS_IGNORE >= translationData.Length - 1)
                    break;
            }

            _screenMessageData = Resources.LoadAll<ScreenMessageData>("ScriptableObjects/Screen Message/Tutorial");

            await Task.Yield();
        }

        public void LoadScreenMessageText()
        {
            for (int i = 0; i < _screenMessageData.Length; i++)
            {
                if (_screenMessageTranslations.ContainsKey(_screenMessageData[i].Key))
                {
                    var textKey = _screenMessageData[i].Key;
                    _screenMessageData[i].SetupText(_screenMessageTranslations[textKey][_clientLanguageIndex]);
                }
            }
        }
    }
}