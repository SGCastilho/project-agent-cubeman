using Cubeman.Utilities;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Projectile
{
    public sealed class ProjectileBehaviour : MonoBehaviour
    {
        #region Encapsulation
        public ProjectileMoviment Moviment { get => moviment; }

        internal ProjectileData Data { get => data; }
        internal ProjectileDamage Damage { get => damage; }
        #endregion

        [Header("Data")]
        [SerializeField] private ProjectileData data;

        [Header("Classes")]
        [SerializeField] private ProjectileMoviment moviment;
        [SerializeField] private ProjectileDamage damage;
        [SerializeField] private DisableObjectOverTime disableObj;

        private void Start() => LoadData();

        private void LoadData()
        {
            moviment.Velocity = data.Velocity;
            damage.Damage = data.Damage;
            damage.DisableOnDamage = data.DisableOnDamage;
            disableObj.DisableTime = data.Range;
        }

        public void ResetTimer() => disableObj.ResetTimer();
    }
}
