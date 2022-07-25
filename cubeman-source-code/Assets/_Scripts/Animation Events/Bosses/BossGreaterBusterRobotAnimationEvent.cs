using Cubeman.Audio;
using Cubeman.Enemies;
using Cubeman.Manager;
using Cubeman.Projectile;
using Cubeman.GameCamera;
using UnityEngine;

namespace Cubeman.AnimationEvents
{
    public sealed class BossGreaterBusterRobotAnimationEvent : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private BossGreaterBusterRobotBehaviour behaviour;

        private CameraShake _cameraShake;
        private AudioController _audioController;
        private ObjectPoolingManager _poolingManager;

        [Header("Settings")]
        [SerializeField] private Transform shootingPointTransform;
        [SerializeField] private Transform shockWavePointTransform;

        [Space(12)]

        [SerializeField] private ParticleSystem shockWaveSparksParticles;
        [SerializeField] private ParticleSystem deathLaserChargeParticles;

        [Space(12)]

        [SerializeField] private GameObject deathLaserGameObject;

        private const string AUDIO_JUMPIN_KEY = "audio_jumpIn";
        private const string AUDIO_JUMPOUT_KEY = "audio_jumpOut";
        private const string AUDIO_FOOTSTEP_KEY = "audio_footstep";
        private const string AUDIO_DEATHLASER_OUT_KEY = "audio_deathLaser_out";
        private const string AUDIO_DEATHLASER_CHARGE_KEY = "audio_deathLaser_charge";

        private string _laserProjectileKey;
        private AudioClip _laserAudioClip;
        private float _laserVolumeScale;

        private string _shockWaveProjectileKey;
        private AudioClip _shockWaveAudioClip;
        private float _shockWaveVolumeScale;

        private AudioClip _footstepAudioClip;
        private float _footstepVolumeScale;

        private AudioClip _deathLaserOutAudioClip;
        private float _deathLaserOutVolumeScale;

        private AudioClip _deathLaserChargeAudioClip;
        private float _deathLaserChargeVolumeScale;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            CacheComponets();
            CacheData();
        }

        private void CacheComponets()
        {
            _cameraShake = CameraShake.Instance;
            _poolingManager = ObjectPoolingManager.Instance;
            _audioController = AudioController.Instance;
        }

        private void CacheData()
        {
            CacheProjectilesSFX();
            CacheDeathLaserSFX();
        }

        private void CacheProjectilesSFX()
        {
            _laserProjectileKey = behaviour.DataLoader.Data.Projectiles[0].Key;
            _laserAudioClip = behaviour.DataLoader.Data.Projectiles[0].ProjectileSFX;
            _laserVolumeScale = behaviour.DataLoader.Data.Projectiles[0].VolumeScale;

            _shockWaveProjectileKey = behaviour.DataLoader.Data.Projectiles[1].Key;
            _shockWaveAudioClip = behaviour.DataLoader.Data.Projectiles[1].ProjectileSFX;
            _shockWaveVolumeScale = behaviour.DataLoader.Data.Projectiles[1].VolumeScale;
        }

        private void CacheDeathLaserSFX()
        {
            var footstepSFX = behaviour.SoundEffects.GetSoundEffect(AUDIO_FOOTSTEP_KEY);

            _footstepAudioClip = footstepSFX._audioClip;
            _footstepVolumeScale = footstepSFX._audioVolumeScale;

            var deathLaserOutSFX = behaviour.SoundEffects.GetSoundEffect(AUDIO_DEATHLASER_OUT_KEY);

            _deathLaserOutAudioClip = deathLaserOutSFX._audioClip;
            _deathLaserOutVolumeScale = deathLaserOutSFX._audioVolumeScale;

            var deathLaserChargeSFX = behaviour.SoundEffects.GetSoundEffect(AUDIO_DEATHLASER_CHARGE_KEY);

            _deathLaserChargeAudioClip = deathLaserChargeSFX._audioClip;
            _deathLaserChargeVolumeScale = deathLaserChargeSFX._audioVolumeScale;
        }

        public void ShootEvent()
        {
            var projectile = _poolingManager.SpawnPrefab(_laserProjectileKey, shootingPointTransform.position)
                .GetComponent<ProjectileBehaviour>();

            projectile.Moviment.MoveRight = behaviour.Movement.MoveRight;

            _audioController.PlaySoundEffect(ref _laserAudioClip, _laserVolumeScale);
        }

        public void ShockWaveSparksEvent()
        {
            shockWaveSparksParticles.Play();
        }

        public void ShockWaveStartEvent()
        {
            behaviour.Movement.CrounchEnemy = true;
        }

        public void ShockWaveEvent()
        {
            _cameraShake.LightShakeCamera();

            var projectile = _poolingManager.SpawnPrefab(_shockWaveProjectileKey, shockWavePointTransform.position)
                .GetComponent<ProjectileBehaviour>();

            projectile.Moviment.MoveRight = behaviour.Movement.MoveRight;

            _audioController.PlaySoundEffect(ref _shockWaveAudioClip, _shockWaveVolumeScale);
        }

        public void ShockWaveEndEvent()
        {
            behaviour.Movement.CrounchEnemy = false;
        }

        public void JumpInEvent()
        {
            var jumpInSFX = behaviour.SoundEffects.GetSoundEffect(AUDIO_JUMPIN_KEY);
            _audioController.PlaySoundEffect(ref jumpInSFX._audioClip, jumpInSFX._audioVolumeScale);
        }

        public void JumpOutEvent()
        {
            _cameraShake.LightShakeCamera();

            var jumpOutSFX = behaviour.SoundEffects.GetSoundEffect(AUDIO_JUMPOUT_KEY);
            _audioController.PlaySoundEffect(ref jumpOutSFX._audioClip, jumpOutSFX._audioVolumeScale);
        }

        public void OffensiveRunningStep()
        {
            _audioController.PlaySoundEffect(ref _footstepAudioClip, _footstepVolumeScale);
            _cameraShake.LightShakeCamera();
        }

        public void DeathLaserStartChargeEvent()
        {
            _audioController.PlaySoundEffect(ref _deathLaserChargeAudioClip, _deathLaserChargeVolumeScale);
            deathLaserChargeParticles.Play();
        }

        public void DeathLaserEndChargeEvent()
        {
            deathLaserChargeParticles.Stop();
        }

        public void DeathLaserStartEvent()
        {
            _audioController.PlaySoundEffect(ref _deathLaserOutAudioClip, _deathLaserOutVolumeScale);
            deathLaserGameObject.SetActive(true);
        }

        public void DeathLaserEndEvent()
        {
            deathLaserGameObject.SetActive(false);
        }
    }
}