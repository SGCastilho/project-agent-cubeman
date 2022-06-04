using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class OptionsManager : MonoBehaviour
    {
        private Resolution clientResolution;
        private Resolution[] supportedResolutions;

        private FullScreenMode clientFullScreenMode;

        private float clientTargetFrameRate;

        private int clientVsync;
        private int clientTextureQuality;

        private AnisotropicFiltering clientAnisotropic;

        private ShadowResolution clientShadowResolution;

        private int clientAntialiasing;

        private void Awake() => CacheCurrentSettings();

        private void CacheCurrentSettings()
        {
            clientResolution = Screen.currentResolution;
            clientFullScreenMode = Screen.fullScreenMode;
            clientTargetFrameRate = Application.targetFrameRate;

            clientVsync = QualitySettings.vSyncCount;

            clientTextureQuality = QualitySettings.masterTextureLimit;
            clientAnisotropic = QualitySettings.anisotropicFiltering;

            clientShadowResolution = QualitySettings.shadowResolution;

            clientAntialiasing = QualitySettings.antiAliasing;

            supportedResolutions = Screen.resolutions;
        }

        public string[] GetResolutions()
        {
            var resolutions = new string[supportedResolutions.Length];

            for (int i = 0; i < supportedResolutions.Length; i++)
            {
                resolutions[i] = $"{supportedResolutions[i].width} x {supportedResolutions[i].height}";
            }

            return resolutions;
        }

        public string[] GetSupportedFullScreenMode()
        {
            var fullscreenMode = new string[4];

            fullscreenMode[0] = FullScreenMode.ExclusiveFullScreen.ToString();
            fullscreenMode[1] = FullScreenMode.FullScreenWindow.ToString();
            fullscreenMode[2] = FullScreenMode.MaximizedWindow.ToString();
            fullscreenMode[3] = FullScreenMode.Windowed.ToString();

            return fullscreenMode;
        }
    }
}
