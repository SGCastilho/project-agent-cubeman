using System.IO;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class OptionsManager : MonoBehaviour
    {
        private struct OptionsData 
        {
            public OptionsData(VideoOptions videoOptions, AudioOptions audioOptions)
            {
                clientVideoOptions = videoOptions;
                clientAudioOptions = audioOptions;
            }

            public VideoOptions clientVideoOptions;
            public AudioOptions clientAudioOptions;
        }

        private const string JSON_NAME = "/config.json";

        [Header("Classes")]
        [SerializeField] private VideoOptionsManager videoOptions;
        [SerializeField] private AudioOptionsManager audioOptions;

        [Header("Settings")]
        [SerializeField] private bool loadOptionsWhenAwake;

        private void Start() => SetupObject();

        private void SetupObject()
        {
            if (!loadOptionsWhenAwake) return;

            LoadOptionsJSON();
        }

        public void SaveOptionsJSON()
        {
            var optionsData = new OptionsData(videoOptions.ClientVideoOptions, audioOptions.ClientAudioOptions);

            var json = JsonUtility.ToJson(optionsData);
            File.WriteAllText(Application.persistentDataPath + JSON_NAME, json);
        }

        public void LoadOptionsJSON()
        {
            if(File.Exists(Application.persistentDataPath + JSON_NAME))
            {
                var json = File.ReadAllText(Application.persistentDataPath + JSON_NAME);

                var optionsData = JsonUtility.FromJson<OptionsData>(json);

                audioOptions.SetAudioOptions(optionsData.clientAudioOptions);
                videoOptions.SetClientOptions(optionsData.clientVideoOptions);

                videoOptions.ApplyClientOptions();
            }
            else
            {
                SaveOptionsJSON();
            }
        }
    }
}
