using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New BossData", menuName = "Scriptable Object/Boss Data", order = 1)]
    public sealed class BossData : ScriptableObject
    {
        #region Encapsulation
        public int Health { get => bossHealth; }
        public ProjectileData[] Projectiles { get => bossProjectiles; }
        #endregion

        [Header("Settings")]
        [SerializeField] private int bossHealth = 60;
        [SerializeField] private ProjectileData[] bossProjectiles;

        #region Editor Variable
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] [Multiline(4)] private string devNotes = "Default Boss Data";
#endif
        #endregion
    }
}