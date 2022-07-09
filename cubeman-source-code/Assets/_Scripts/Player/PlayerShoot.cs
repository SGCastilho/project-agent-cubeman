using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Player
{
    public sealed class PlayerShoot : MonoBehaviour
    {
        #region Encapsulation
        public ProjectileData ProjectileData { get => projectile; }
        public ProjectileData UltimateData { get => ultimateProjectile; }

        internal bool BlockShooting { set => _blockShooting = value; }
        #endregion

        [Header("Data")]
        [SerializeField] private ProjectileData projectile;
        [SerializeField] private ProjectileData ultimateProjectile;

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float fireRate = 0.2f;

        private bool _isShooting;
        private bool _blockShooting;

        private float _currentFireRateDuration;

        internal void Shooting()
        {
            if (_blockShooting) return;

            if(!_isShooting)
            {
                behaviour.Animation.CallAnimatorTrigger("shoot");
                _isShooting = true;
            }
        }

        private void Update() => FireRateTimer();

        private void FireRateTimer()
        {
            if (!_isShooting) return;

            _currentFireRateDuration += Time.deltaTime;
            if (_currentFireRateDuration >= fireRate)
            {
                _isShooting = false;
                _currentFireRateDuration = 0;
            }
        }

        internal void UltimateShooting()
        {
            if (_blockShooting) return;

            if (behaviour.Status.UltimateReady)
            {
                behaviour.BlockAction(true);
                behaviour.Status.InvensibleMode = true;
                behaviour.Moviment.Gravity.FreezeGravity = true;
                behaviour.Animation.CallAnimatorTrigger("ultimate");
            }
        }
    }
}
