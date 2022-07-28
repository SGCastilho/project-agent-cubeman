using TMPro;
using System.Text;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIFPSCounter : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private TextMeshProUGUI tmpFpsCounter;

        private StringBuilder _frameRateStringBuilder = new StringBuilder();

        public void RefreshFPS(ref int frameRate)
        {
            _frameRateStringBuilder.Length = 0;

            _frameRateStringBuilder.Append(frameRate);

            tmpFpsCounter.text = _frameRateStringBuilder.ToString();
        }
    }
}