using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New UI Text Message", menuName = "Scriptable Object/UI Text Message")]
    public sealed class UITextMessageData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => key; }
        public string Message { get => message; }
        #endregion

        [Header("Settings")]
        [SerializeField] private string key = "Put the translation key here.";

        [Space(6)]

        [SerializeField] private string message;


        #region Editor Variable
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] [Multiline(6)] private string devNotes = "Put your dev notes here.";
#endif
        #endregion

        public void SetMessage(string text)
        {
            message = text;
        }
    }
}