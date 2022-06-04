using Cubeman.UI;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class OptionsEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private OptionsManager optionsManager;

        [Space(12)]

        [SerializeField] private UIVideoOptions uiVideoOptions;
        [SerializeField] private UIButtonsVideoOptions uiButtonsVideoOptions;

        private void OnEnable() => EnableEvents();

        private void OnDisable() => DisableEvents();

        private void EnableEvents()
        {
            uiVideoOptions.OnGetClientOptions += optionsManager.GetClientOptions;

            uiVideoOptions.OnGetSupportedScreen += optionsManager.GetSupportedFullScreenMode;
            uiVideoOptions.OnGetSupportedResolution += optionsManager.GetSupportedResolutions;

            uiButtonsVideoOptions.OnSetNewVideoOptions += optionsManager.SetClientOptions;
            uiButtonsVideoOptions.OnApplyVideoOptions += optionsManager.ApplyClientOptions;
        }

        private void DisableEvents()
        {
            uiVideoOptions.OnGetClientOptions -= optionsManager.GetClientOptions;

            uiVideoOptions.OnGetSupportedScreen -= optionsManager.GetSupportedFullScreenMode;
            uiVideoOptions.OnGetSupportedResolution -= optionsManager.GetSupportedResolutions;

            uiButtonsVideoOptions.OnSetNewVideoOptions -= optionsManager.SetClientOptions;
            uiButtonsVideoOptions.OnApplyVideoOptions -= optionsManager.ApplyClientOptions;
        }
    }
}