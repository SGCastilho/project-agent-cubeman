using Cubeman.UI;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class OptionsEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private OptionsManager optionsManager;
        [SerializeField] private VideoOptionsManager videoOptionsManager;
        [SerializeField] private AudioOptionsManager audioOptionsManager;

        [Space(12)]

        [SerializeField] private UIVideoOptions uiVideoOptions;
        [SerializeField] private UIButtonsVideoOptions uiButtonsVideoOptions;

        [Space(6)]

        [SerializeField] private UIAudioOptions uiAudioOptions;

        private void OnEnable() => EnableEvents();

        private void OnDisable() => DisableEvents();

        private void EnableEvents()
        {
            audioOptionsManager.OnNewOptionsApply += optionsManager.SaveOptionsJSON;

            videoOptionsManager.OnRefreshOptionsUI += uiVideoOptions.CheckVideoOptions;
            videoOptionsManager.OnNewOptionsApply += optionsManager.SaveOptionsJSON;

            uiVideoOptions.OnGetClientOptions += videoOptionsManager.GetClientOptions;

            uiVideoOptions.OnGetSupportedScreen += videoOptionsManager.GetSupportedFullScreenMode;
            uiVideoOptions.OnGetSupportedResolution += videoOptionsManager.GetSupportedResolutions;

            uiButtonsVideoOptions.OnSetNewVideoOptions += videoOptionsManager.SetClientOptions;
            uiButtonsVideoOptions.OnApplyVideoOptions += videoOptionsManager.ApplyClientOptions;

            uiAudioOptions.OnGetSoundTrackVolume += audioOptionsManager.GetSoundTrackVolume;
            uiAudioOptions.OnGetSoundEffectVolume += audioOptionsManager.GetSoundEffectVolume;
            uiAudioOptions.OnSetSoundTrackVolume += audioOptionsManager.SetSoundTrackVolume;
            uiAudioOptions.OnSetSoundEffectVolume += audioOptionsManager.SetSoundEffectVolume;
        }

        private void DisableEvents()
        {
            audioOptionsManager.OnNewOptionsApply -= optionsManager.SaveOptionsJSON;

            videoOptionsManager.OnRefreshOptionsUI -= uiVideoOptions.CheckVideoOptions;
            videoOptionsManager.OnNewOptionsApply -= optionsManager.SaveOptionsJSON;

            uiVideoOptions.OnGetClientOptions -= videoOptionsManager.GetClientOptions;

            uiVideoOptions.OnGetSupportedScreen -= videoOptionsManager.GetSupportedFullScreenMode;
            uiVideoOptions.OnGetSupportedResolution -= videoOptionsManager.GetSupportedResolutions;

            uiButtonsVideoOptions.OnSetNewVideoOptions -= videoOptionsManager.SetClientOptions;
            uiButtonsVideoOptions.OnApplyVideoOptions -= videoOptionsManager.ApplyClientOptions;

            uiAudioOptions.OnGetSoundTrackVolume -= audioOptionsManager.GetSoundTrackVolume;
            uiAudioOptions.OnGetSoundEffectVolume -= audioOptionsManager.GetSoundEffectVolume;
            uiAudioOptions.OnSetSoundTrackVolume -= audioOptionsManager.SetSoundTrackVolume;
            uiAudioOptions.OnSetSoundEffectVolume -= audioOptionsManager.SetSoundEffectVolume;
        }
    }
}