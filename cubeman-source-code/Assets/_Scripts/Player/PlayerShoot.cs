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

        private bool _canShoot;
        private bool _isShooting;
        private bool _blockShooting;

        private float _currentFireRateDuration;

        internal void ShootingInput()
        {
            if (_blockShooting) return;

            _isShooting = true;
        }

        internal void Shooting()
        {
            if (_blockShooting || !_isShooting) return;

            if(!_canShoot)
            {
                behaviour.Animation.CallAnimatorTrigger("shoot");
                _canShoot = true;
            }
        }

        private void Update() 
        {
            Shooting();
            FireRateTimer();
        }

        private void FireRateTimer()
        {
            if (!_canShoot) return;

            _currentFireRateDuration += Time.deltaTime;
            if (_currentFireRateDuration >= fireRate)
            {
                _canShoot = false;
                _currentFireRateDuration = 0;
            }
        }

        internal void EndShooting()
        {
            _isShooting = false;
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
