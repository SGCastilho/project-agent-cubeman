using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cubeman.UI
{
    public sealed class UIButtonHighlight : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private Image highlightImage;
        [SerializeField] private TextMeshProUGUI highlightTMP;

        [Header("Settings")]
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color highlightColor = Color.black;

        public void Highlight()
        {
            if(highlightTMP != null)
            {
                highlightTMP.color = highlightColor;
            }

            if(highlightImage != null)
            {
                highlightImage.color = highlightColor;
            }
        }

        public void UnHighlight()
        {
            if (highlightTMP != null)
            {
                highlightTMP.color = defaultColor;
            }

            if (highlightImage != null)
            {
                highlightImage.color = defaultColor;
            }
        }
    }
}
