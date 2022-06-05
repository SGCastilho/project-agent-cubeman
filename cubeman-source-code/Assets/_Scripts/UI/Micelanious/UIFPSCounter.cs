using TMPro;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIFPSCounter : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private TextMeshProUGUI tmpFpsCounter;

        public void RefreshFPS(ref int frameRate)
        {
            tmpFpsCounter.text = $"{frameRate}";
        }
    }
}