using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Collectable Resource Data", menuName = "Scriptable Object/Collectable/Resource")]
    public sealed class CollectableResourceData : CollectableData
    {
        #region Encapsulation
        public int Resources { get => amountResources; }
        #endregion

        [Header("Resource Settings")]
        [SerializeField] [Range(2, 20)] private int amountResources = 2;
    }
}
