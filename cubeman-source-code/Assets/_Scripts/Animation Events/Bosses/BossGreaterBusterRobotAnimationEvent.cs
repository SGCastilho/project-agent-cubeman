using Cubeman.Audio;
using Cubeman.Enemies;
using Cubeman.Manager;
using Cubeman.Projectile;
using UnityEngine;

namespace Cubeman.AnimationEvents
{
    public sealed class BossGreaterBusterRobotAnimationEvent : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        private AudioController audioController;
        private ObjectPoolingManager poolingManager;

        [Header("Settings")]
        [SerializeField] private Transform shootingPointTransform;
        [SerializeField] private Transform shockWavePointTransform;

        private string _laserProjectileKey;
        private AudioClip _laserAudioClip;
        private float _laserVolumeScale;

        private string _shockWaveProjectileKey;
        private AudioClip _shockWaveAudioClip;
        private float _shockWaveVolumeScale;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            CacheComponets();
            CacheData();
        }

        private void CacheData()
        {
            _laserProjectileKey = behaviour.DataLoader.Data.Projectiles[0].Key;
            _laserAudioClip = behaviour.DataLoader.Data.Projectiles[0].ProjectileSFX;
            _laserVolumeScale = behaviour.DataLoader.Data.Projectiles[0].VolumeScale;

            _shockWaveProjectileKey = behaviour.DataLoader.Data.Projectiles[1].Key;
            _shockWaveAudioClip = behaviour.DataLoader.Data.Projectiles[1].ProjectileSFX;
            _shockWaveVolumeScale = behaviour.DataLoader.Data.Projectiles[1].VolumeScale;
        }

        private void CacheComponets()
        {
            poolingManager = ObjectPoolingManager.Instance;
            audioController = AudioController.Instance;
        }

        public void ShootEvent()
        {
            var projectile = poolingManager.SpawnPrefab(_laserProjectileKey, shootingPointTransform.position)
                .GetComponent<ProjectileBehaviour>();

            projectile.Moviment.MoveRight = behaviour.Movement.MoveRight;

            audioController.PlaySoundEffect(ref _laserAudioClip, _laserVolumeScale);
        }

        public void ShockWaveEvent()
        {
            var projectile = poolingManager.SpawnPrefab(_shockWaveProjectileKey, shockWavePointTransform.position)
                .GetComponent<ProjectileBehaviour>();

            projectile.Moviment.MoveRight = behaviour.Movement.MoveRight;

            audioController.PlaySoundEffect(ref _shockWaveAudioClip, _shockWaveVolumeScale);
        }

        public void JumpInEvent()
        {

        }

        public void JumpOutEvent()
        {

        }

        public void DangerAttackWarningEvent()
        {

        }

        public void DeathLaserStartEvent()
        {

        }

        public void DeathLaserEndEvent()
        {

        }
    }
}