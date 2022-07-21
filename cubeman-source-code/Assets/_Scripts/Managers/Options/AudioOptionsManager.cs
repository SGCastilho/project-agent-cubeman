using UnityEngine;
using UnityEngine.Audio;

namespace Cubeman.Manager
{
    public sealed class AudioOptionsManager : MonoBehaviour
    {
        #region Encapsulation
        public AudioOptions ClientAudioOptions { get => clientAudioOptions; }
        #endregion

        public delegate void NewOptionsApply();
        public event NewOptionsApply OnNewOptionsApply;

        [Header("Classes")]
        [SerializeField] private AudioMixer soundTrackMixer;
        [SerializeField] private AudioMixer soundEffectMixer;

        private AudioOptions clientAudioOptions;

        private void Awake() => CacheCurrentSettings();

        public void CacheCurrentSettings()
        {
            soundTrackMixer.GetFloat("MasterVolume", out clientAudioOptions.clientSoundTrackVolume);
            soundEffectMixer.GetFloat("MasterVolume", out clientAudioOptions.clientSoundEffectVolume);
        }

        public float GetSoundTrackVolume()
        {
            return clientAudioOptions.clientSoundTrackVolume;
        }

        public float GetSoundEffectVolume()
        {
            return clientAudioOptions.clientSoundEffectVolume;
        }

        public void SetAudioOptions(AudioOptions newAudioOptions)
        {
            clientAudioOptions = newAudioOptions;

            soundTrackMixer.SetFloat("MasterVolume", newAudioOptions.clientSoundTrackVolume);
            soundEffectMixer.SetFloat("MasterVolume", newAudioOptions.clientSoundEffectVolume);
        }

        public void SetSoundTrackVolume(float volume)
        {
            var setupVolume = volume - 60;
            soundTrackMixer.SetFloat("MasterVolume", setupVolume);

            clientAudioOptions.clientSoundTrackVolume = setupVolume;
        }

        public void SetSoundEffectVolume(float volume)
        {
            var setupVolume = volume - 60;
            soundEffectMixer.SetFloat("MasterVolume", setupVolume);

            clientAudioOptions.clientSoundEffectVolume = setupVolume;

            OnNewOptionsApply?.Invoke();
        }
    }
}