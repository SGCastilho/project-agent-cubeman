using TMPro;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UILoadTranslatedText : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private UITextMessageData textMessageData;
        [SerializeField] private UITextMessageData[] dropdownMessageData;

        [Header("Classes")]
        [SerializeField] private TextMeshProUGUI tmpComponent;
        [SerializeField] private TMP_Dropdown tmpDropdown;

        [Header("Settings")]
        [SerializeField] private bool startDropdownTranslationDown;

        private void Start() => LoadTranslatedText();

        private void LoadTranslatedText()
        {
            if(tmpComponent != null)
            {
                tmpComponent.text = textMessageData.Message;
            }

            if(tmpDropdown != null)
            {
                var options = tmpDropdown.options;

                var optionsInteractions = 0;

                if(startDropdownTranslationDown)
                {
                    for(int i = 0; i < dropdownMessageData.Length; i++)
                    {
                        for (int j = options.Count-1; j > 0; j--)
                        {
                            optionsInteractions++;
                            options[j].text = dropdownMessageData[i].Message;

                            if (optionsInteractions >= dropdownMessageData.Length)
                                break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dropdownMessageData.Length; i++)
                    {
                        options[i].text = dropdownMessageData[i].Message;
                    }
                }
            }
        }
    }
}