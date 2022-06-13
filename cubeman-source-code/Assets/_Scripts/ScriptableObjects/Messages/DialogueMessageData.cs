using TMPro;
using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [System.Serializable]
    public struct DialogueSequence
    {
        #region Encapsulation
        public string CharacterName { get => characterName; }
        public Sprite CharacterSprite { get => characterFace; }
        public string CharacterDialogue { get => characterDialogue; set => characterDialogue = value; }
        #endregion

        [SerializeField] private string characterName;
        [SerializeField] private Sprite characterFace;
        [SerializeField] [Multiline(6)] private string characterDialogue;
    }

    [CreateAssetMenu(fileName = "New Dialogue Message Data", menuName = "Scriptable Object/Dialogue/Dialogue Message", order = 0)]
    public sealed class DialogueMessageData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => key; }

        public DialogueSequence[] DialogueSequence { get => dialogueSequence; }
        public TMP_SpriteAsset SpriteAsset { get => hotkeysSpriteAsset; }
        #endregion

        [Header("Settings")]
        [SerializeField] private string key = "Put the translation Key here.";

        [SerializeField] private DialogueSequence[] dialogueSequence;

        [Space(8)]

        [SerializeField] private TMP_SpriteAsset hotkeysSpriteAsset;

        public void SetupDialoguesText(string[] translation)
        {
            for (int i = 0; i < dialogueSequence.Length; i++)
            {
                dialogueSequence[i].CharacterDialogue = translation[i];
            }
        }

        #region Editor Variables
    #if UNITY_EDITOR

        [Space(8)]

        [Header("Editor Settings")]
        [SerializeField] [Multiline(6)] private string devNotes = "Put your dev notes here.";
    #endif
        #endregion
    }
}
