using TMPro;
using System.Collections.Generic;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIVideoOptions : MonoBehaviour
    {
        #region Encapsulation
        internal VideoOptions CurrentVideoOptions { get => _currentVideoOptions; }
        internal VideoOptions NewVideoOptions { get => _newVideoOptions; }
        #endregion

        public delegate string[] SupportedResolution();
        public event SupportedResolution OnGetSupportedResolution;

        public delegate string[] SupportedScreen();
        public event SupportedScreen OnGetSupportedScreen;

        public delegate VideoOptions ClientOptions();
        public event ClientOptions OnGetClientOptions;

        private VideoOptions _currentVideoOptions;
        private VideoOptions _newVideoOptions;

        [Header("Classes")]
        [SerializeField] private TMP_Dropdown resolutionDropDown;
        [SerializeField] private TMP_Dropdown screenDropDown;
        [SerializeField] private TMP_Dropdown frameRateDropDown;
        [SerializeField] private TMP_Dropdown vSyncDropDown;
        [SerializeField] private TMP_Dropdown textureDropDown;
        [SerializeField] private TMP_Dropdown shadowDropDown;
        [SerializeField] private TMP_Dropdown anisotropicDropDown;
        [SerializeField] private TMP_Dropdown antialiasingDropDown;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            SetupResolutions();
            SetupScreen();
            SetupClientOptions();
        }

        private void SetupResolutions()
        {
            var resolutionArray = OnGetSupportedResolution?.Invoke();
            List<string> supportedResolutions = new List<string>();

            for (int i = 0; i < resolutionArray.Length; i++)
            {
                supportedResolutions.Add(resolutionArray[i]);
            }

            resolutionDropDown.AddOptions(supportedResolutions);
            resolutionDropDown.value = resolutionArray.Length;
        }

        private void SetupScreen()
        {
            var screenArray = OnGetSupportedScreen?.Invoke();
            List<string> supportedScreen = new List<string>();

            for (int i = 0; i < screenArray.Length; i++)
            {
                supportedScreen.Add(screenArray[i]);
            }

            screenDropDown.AddOptions(supportedScreen);
        }

        internal void SetupClientOptions()
        {
            _currentVideoOptions = OnGetClientOptions();

            resolutionDropDown.value = _currentVideoOptions.clientResolutionIndex;
            screenDropDown.value = _currentVideoOptions.clientFullScreenModeIndex;

            frameRateDropDown.value = GetTargetFrameRate();

            vSyncDropDown.value = _currentVideoOptions.clientVsync;
            
            textureDropDown.value = _currentVideoOptions.clientTextureQuality;
            anisotropicDropDown.value = ((int)_currentVideoOptions.clientAnisotropic);

            shadowDropDown.value = _currentVideoOptions.clientShadowQuality;

            antialiasingDropDown.value = GetAntialiasingValue();
        }

        private int GetAntialiasingValue()
        {
            int antialiasing = 0;

            switch(_currentVideoOptions.clientAntialiasing)
            {
                case 1:
                    antialiasing = 0;
                    break;
                case 2:
                    antialiasing = 1;
                    break;
                case 4:
                    antialiasing = 2;
                    break;
                case 8:
                    antialiasing = 3;
                    break;
            }

            return antialiasing;
        }

        private int GetTargetFrameRate()
        {
            int frameRateIndex = 0;

            if (_currentVideoOptions.clientTargetFrameRate < 0)
            {
                frameRateIndex = 5;
            }
            else
            {
                List<TMP_Dropdown.OptionData> frameRateOptions = new List<TMP_Dropdown.OptionData>();

                for (int i = 0; i < frameRateOptions.Count; i++)
                {
                    if (frameRateOptions[i].text == _currentVideoOptions.clientTargetFrameRate.ToString())
                    {
                        frameRateIndex = i;
                        break;
                    }
                }
            }

            return frameRateIndex;
        }

        internal void CheckVideoOptions()
        {
            _newVideoOptions.clientResolutionIndex = resolutionDropDown.value;
            _newVideoOptions.clientFullScreenModeIndex = screenDropDown.value;

            _newVideoOptions.clientTargetFrameRate = GetTargetFrameRateValue();

            _newVideoOptions.clientVsync = vSyncDropDown.value;

            _newVideoOptions.clientTextureQuality = textureDropDown.value;
            _newVideoOptions.clientAnisotropic = (AnisotropicFiltering)anisotropicDropDown.value;

            _newVideoOptions.clientShadowQuality = shadowDropDown.value;

            _newVideoOptions.clientAntialiasing = GetAntiAliasingValue();
        }

        private int GetTargetFrameRateValue()
        {
            int frameRateValue = 0;

            if (frameRateDropDown.value == 5)
            {
                frameRateValue = -1;
            }
            else
            {
                List<TMP_Dropdown.OptionData> frameRateOptions = new List<TMP_Dropdown.OptionData>();
                frameRateOptions = frameRateDropDown.options;

                for (int i = 0; i < frameRateOptions.Count; i++)
                {
                    if (frameRateOptions[i] == frameRateDropDown.options[frameRateDropDown.value])
                    {
                        frameRateValue = int.Parse(frameRateOptions[i].text);
                        break;
                    }
                }
            }

            return frameRateValue;
        }

        private int GetAntiAliasingValue()
        {
            int antiAliasingValue = 0;

            if (antialiasingDropDown.value == 0)
            {
                antiAliasingValue = 1;
            }
            else
            {
                List<TMP_Dropdown.OptionData> antiAliasingOptions = new List<TMP_Dropdown.OptionData>();
                antiAliasingOptions = antialiasingDropDown.options;

                for (int i = 0; i < antiAliasingOptions.Count; i++)
                {
                    if (antiAliasingOptions[i] == antialiasingDropDown.options[antialiasingDropDown.value])
                    {
                        antiAliasingValue = int.Parse(antiAliasingOptions[i].text);
                        break;
                    }
                }
            }

            return antiAliasingValue;
        }
    }
}