using TMPro;
using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Dialogue Message Data", menuName = "Scriptable Object/Dialogue/Dialogue Message", order = 0)]
    public sealed class DialogueMessageData : ScriptableObject
    {
        #region Encapsulation
        public Sprite CharacterSprite { get => characterSprite; }

        public string CharacterName { get => characterName; }
        public string Dialogue { get => dialogue; }

        public TMP_SpriteAsset SpriteAsset { get => hotkeysSpriteAsset; }
        #endregion

        [Header("Settings")]
        [SerializeField] private Sprite characterSprite;

        [Space(12)]

        [SerializeField] private string characterName = "Cubeman";
        [SerializeField] [Multiline(6)] private string dialogue = "Put your dialogue message here.";

        [Space(12)]

        [SerializeField] private TMP_SpriteAsset hotkeysSpriteAsset;

        #region Editor Variables
    #if UNITY_EDITOR

        [Space(12)]

        [Header("Editor Settings")]
        [SerializeField] [Multiline(6)] private string devNotes = "Put your dev notes here.";
    #endif
        #endregion
    }
}
