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
        public string CharacterDialogue { get => characterDialogue; }
        #endregion

        [SerializeField] private string characterName;
        [SerializeField] private Sprite characterFace;
        [SerializeField] [Multiline(6)] private string characterDialogue;
    }

    [CreateAssetMenu(fileName = "New Dialogue Message Data", menuName = "Scriptable Object/Dialogue/Dialogue Message", order = 0)]
    public sealed class DialogueMessageData : ScriptableObject
    {
        #region Encapsulation
        public DialogueSequence[] DialogueSequence { get => dialogueSequence; }
        public TMP_SpriteAsset SpriteAsset { get => hotkeysSpriteAsset; }
        #endregion

        [Header("Settings")]
        [SerializeField] private DialogueSequence[] dialogueSequence;

        [Space(8)]

        [SerializeField] private TMP_SpriteAsset hotkeysSpriteAsset;

        #region Editor Variables
    #if UNITY_EDITOR

        [Space(8)]

        [Header("Editor Settings")]
        [SerializeField] [Multiline(6)] private string devNotes = "Put your dev notes here.";
    #endif
        #endregion
    }
}
