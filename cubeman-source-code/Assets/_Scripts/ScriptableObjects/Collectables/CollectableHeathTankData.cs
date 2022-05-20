using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Collectable Heath Tank Data", menuName = "Scriptable Object/Collectable/Health Tank")]
    public sealed class CollectableHeathTankData : CollectableData
    {
        #region Encapsulation
        public float RecoveryPercentage { get => recoveryPercentage; }
        #endregion

        [Header("Health Tank Settings")]
        [Tooltip("0.1f = 10% of max health, 1f = 100% of max health")]
        [SerializeField] [Range(0.1f, 1f)] private float recoveryPercentage = 0.2f;
    }
}
