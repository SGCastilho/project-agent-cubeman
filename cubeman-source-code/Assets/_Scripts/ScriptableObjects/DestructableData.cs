using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Destructable Data", menuName = "Scriptable Object/Destructable Data")]
    public sealed class DestructableData : ScriptableObject
    {
        #region Encapsulation
        public int Health { get => destructableHealth; }
        #endregion

        [Header("Settings")]
        [SerializeField] [Range(6, 20)] private int destructableHealth = 12;
    }
}
