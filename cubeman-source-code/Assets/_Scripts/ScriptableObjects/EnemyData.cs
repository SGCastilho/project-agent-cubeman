using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Object/Enemy Data", order = 2)]
    public sealed class EnemyData : ScriptableObject
    {
        #region Encapsulation
        public int Health { get => enemyHealth; }
        public ProjectileData Projectile { get => enemyProjectile; }
        #endregion

        [Header("Settings")]
        [SerializeField] private int enemyHealth = 60;
        [SerializeField] private ProjectileData enemyProjectile;

        #region Editor Variable
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] [Multiline(4)] private string devNotes = "Default Enemy Data";
#endif
        #endregion
    }
}
