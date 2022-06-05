using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Cubeman.Manager
{
    public sealed class VideoOptionsManager : MonoBehaviour
    {
        private VideoOptions _clientVideoOptions;
        private Resolution[] _supportedResolutions;

        [Header("Pipeline Asset")]
        [SerializeField] private UniversalRenderPipelineAsset pipelineAsset;

        private void Awake() => CacheCurrentSettings();

        private void CacheCurrentSettings()
        {
            _supportedResolutions = Screen.resolutions;

            _clientVideoOptions.clientResolution = Screen.currentResolution;

            CacheResolutionIndex();

            _clientVideoOptions.clientFullScreenMode = Screen.fullScreenMode;
            _clientVideoOptions.clientFullScreenModeIndex = ((int)Screen.fullScreenMode);

            _clientVideoOptions.clientTargetFrameRate = Application.targetFrameRate;

            _clientVideoOptions.clientVsync = QualitySettings.vSyncCount;

            _clientVideoOptions.clientTextureQuality = QualitySettings.masterTextureLimit;
            _clientVideoOptions.clientAnisotropic = QualitySettings.anisotropicFiltering;

            _clientVideoOptions.clientAntialiasing = pipelineAsset.msaaSampleCount;
        }

        private void CacheResolutionIndex()
        {
            for (int i = 0; i < _supportedResolutions.Length; i++)
            {
                if (_supportedResolutions[i].height == _clientVideoOptions.clientResolution.height)
                {
                    _clientVideoOptions.clientResolutionIndex = i;
                    break;
                }
            }
        }

        public string[] GetSupportedResolutions()
        {
            var resolutions = new string[_supportedResolutions.Length];

            for (int i = 0; i < _supportedResolutions.Length; i++)
            {
                resolutions[i] = $"{_supportedResolutions[i].width} x {_supportedResolutions[i].height}";
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

        public VideoOptions GetClientOptions()
        {
            return _clientVideoOptions;
        }

        public void SetClientOptions(VideoOptions newOptions) 
        {
            _clientVideoOptions = newOptions;
        }

        public void ApplyClientOptions()
        {
            _clientVideoOptions.clientResolution = _supportedResolutions[_clientVideoOptions.clientResolutionIndex];
            _clientVideoOptions.clientFullScreenMode = (FullScreenMode)_clientVideoOptions.clientFullScreenModeIndex;

            var resolutionWidth = _clientVideoOptions.clientResolution.width;
            var resolutionHeight = _clientVideoOptions.clientResolution.height;

            Screen.SetResolution(resolutionWidth, resolutionHeight, _clientVideoOptions.clientFullScreenMode);

            Application.targetFrameRate = _clientVideoOptions.clientTargetFrameRate;

            QualitySettings.vSyncCount = _clientVideoOptions.clientVsync;

            QualitySettings.masterTextureLimit = _clientVideoOptions.clientTextureQuality;
            QualitySettings.anisotropicFiltering = _clientVideoOptions.clientAnisotropic;

            pipelineAsset.msaaSampleCount = _clientVideoOptions.clientAntialiasing;
        }
    }
}
