using UnityEngine;
using UnityEngine.UI;

namespace Cubeman.UI
{
    public sealed class UIAudioOptions : MonoBehaviour
    {
        public delegate float GetSoundTrackVolume();
        public event GetSoundTrackVolume OnGetSoundTrackVolume;

        public delegate float GetSoundEffectVolume();
        public event GetSoundEffectVolume OnGetSoundEffectVolume;

        public delegate void SetSoundTrackVolume(float volume);
        public event SetSoundTrackVolume OnSetSoundTrackVolume;

        public delegate void SetSoundEffectVolume(float volume);
        public event SetSoundEffectVolume OnSetSoundEffectVolume;

        [Header("Classes")]
        [SerializeField] private Slider soundTrackSlider;
        [SerializeField] private Slider soundEffectSlider;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            ResetToCurrentSettings();
        }

        private void SetupSlider(Slider slider, float value)
        {
            var mixerVolume = value;
            var setupVolume = mixerVolume + 80;

            slider.value = setupVolume;
        }

        public void SaveAudioSettings()
        {
            OnSetSoundTrackVolume?.Invoke(soundTrackSlider.value);
            OnSetSoundEffectVolume?.Invoke(soundEffectSlider.value);
        }

        public void ResetToCurrentSettings()
        {
            SetupSlider(soundTrackSlider, OnGetSoundTrackVolume());
            SetupSlider(soundEffectSlider, OnGetSoundEffectVolume());
        }
    }
}