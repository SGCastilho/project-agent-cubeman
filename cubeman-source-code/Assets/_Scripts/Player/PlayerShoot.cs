using System.Collections;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Player
{
    public sealed class PlayerShoot : MonoBehaviour
    {
        #region Encapsulation
        public ProjectileData ProjectileData { get => projectile; }
        public ProjectileData UltimateData { get => ultimateProjectile; }
        #endregion

        [Header("Data")]
        [SerializeField] private ProjectileData projectile;
        [SerializeField] private ProjectileData ultimateProjectile;

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fireRate = 0.2f;

        private bool _isShooting;

        internal void Shooting()
        {
            if(!_isShooting)
            {
                behaviour.Animation.CallAnimatorTrigger("shoot");
                StartCoroutine(FireRateCourotine());
            }
        }

        internal void UltimateShooting()
        {
            if(behaviour.Status.UltimateReady)
            {
                behaviour.Status.InvensibleMode = true;
                behaviour.Input.GameplayInputs(false);
                behaviour.Moviment.Gravity.FreezeGravity = true;
                behaviour.Animation.CallAnimatorTrigger("ultimate");
            }
        }

        IEnumerator FireRateCourotine()
        {
            _isShooting = true;
            yield return new WaitForSeconds(fireRate);
            _isShooting = false;
        }
    }
}
