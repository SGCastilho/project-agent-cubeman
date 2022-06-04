using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIButtonsVideoOptions : MonoBehaviour
    {
        public delegate void SetNewVideoOptions(VideoOptions newClientVideoOptions);
        public event SetNewVideoOptions OnSetNewVideoOptions;

        public delegate void ApplyVideoOptions();
        public event ApplyVideoOptions OnApplyVideoOptions;

        [Header("Classes")]
        [SerializeField] private UIVideoOptions uiVideoOptions;

        public void SaveVideoOptions()
        {
            uiVideoOptions.CheckVideoOptions();

            OnSetNewVideoOptions?.Invoke(uiVideoOptions.NewVideoOptions);
            OnApplyVideoOptions?.Invoke();
        }

        public void CancelVideoOptions()
        {
            //Reset UI
        }
    }
}