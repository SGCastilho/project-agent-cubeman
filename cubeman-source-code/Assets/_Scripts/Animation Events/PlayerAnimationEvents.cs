using Cubeman.Audio;
using Cubeman.Player;
using Cubeman.Manager;
using Cubeman.Projectile;
using Cubeman.GameCamera;
using UnityEngine;

namespace Cubeman.AnimationEvents
{
    public sealed class PlayerAnimationEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;

        private CameraShake _cameraShake;
        private AudioController _audioManager;
        private ObjectPoolingManager _poolingManager;

        private ProjectileBehaviour _currentProjectile;

        [Header("Settings")]
        [SerializeField] private Transform shootingPointTransform;

        [Space(12)]

        [SerializeField] private ParticleSystem plasmaMuzzleParticle;

        private Transform _muzzleParticleTransform;

        private string _projectileKey;
        private AudioClip _projectileAudioClip;
        private float _projectileAudioClipVolumeScale;

        private string _ultimateProjectileKey;
        private AudioClip _ultimateAudioClip;
        private float _ultimateAudioClipVolumeScale;

        private void Awake() => CacheComponets();

        private void CacheComponets()
        {
            _cameraShake = CameraShake.Instance;
            _audioManager = AudioController.Instance;
            _poolingManager = ObjectPoolingManager.Instance;

            _muzzleParticleTransform = plasmaMuzzleParticle.GetComponent<Transform>();
        }

        private void OnEnable() => GetProjectileData();

        private void GetProjectileData()
        {
            _projectileKey = behaviour.Shoot.ProjectileData.Key;
            _projectileAudioClipVolumeScale = behaviour.Shoot.ProjectileData.VolumeScale;
            _projectileAudioClip = behaviour.Shoot.ProjectileData.ProjectileSFX;

            _ultimateProjectileKey = behaviour.Shoot.UltimateData.Key;
            _ultimateAudioClipVolumeScale = behaviour.Shoot.UltimateData.VolumeScale;
            _ultimateAudioClip = behaviour.Shoot.UltimateData.ProjectileSFX;
        }

        public void MuzzleEffect()
        {
            CorrectMuzzleRotation();

            plasmaMuzzleParticle.Play();
        }

        private void CorrectMuzzleRotation()
        {
            if (behaviour.Moviment.MoveRight)
            {
                _muzzleParticleTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                _muzzleParticleTransform.localEulerAngles = new Vector3(0f, 180f, 0f);
            }
        }

        public void ShootEvent()
        {
            InstantiateProjectile(_projectileKey);

            _audioManager.PlaySoundEffect(ref _projectileAudioClip, _projectileAudioClipVolumeScale);
        }

        public void ShootUltimateEvent()
        {
            behaviour.BlockAction(false);

            behaviour.Moviment.Gravity.FreezeGravity = false;

            behaviour.Status.InvensibleMode = false;
            behaviour.Status.UltimateReady = false;

            _cameraShake.ShakeCamera();

            InstantiateProjectile(_ultimateProjectileKey);

            _audioManager.PlaySoundEffect(ref _ultimateAudioClip, _ultimateAudioClipVolumeScale);
        }

        private void InstantiateProjectile(string projectileKey)
        {
            _currentProjectile = _poolingManager.SpawnPrefab(projectileKey, shootingPointTransform.position)
                .GetComponent<ProjectileBehaviour>();

            _currentProjectile.Moviment.MoveRight = behaviour.Moviment.GetCurrentGraphicsFlippedSide();
            _currentProjectile.ResetObject();
        }

        public void StaggerShake() => _cameraShake.LightShakeCamera();

        public void StaggerEvent()
        {
            StaggerShake();

            behaviour.Moviment.StartCoroutine(behaviour.Moviment.TakeDamageImpulseCoroutine());

            StaggerAudioEvent();
        }

        public void DashAudioEvent() => PlaySFX(behaviour.Moviment.DashSFX);

        public void JumpInAudioEvent() => PlaySFX(behaviour.Moviment.JumpInSFX);

        public void JumpOutAudioEvent() => PlaySFX(behaviour.Moviment.JumpOutSFX);

        public void StaggerAudioEvent() => PlaySFX(behaviour.Status.StaggerSFX);

        private void PlaySFX(string audioKey)
        {
            var audioList = behaviour.SoundEffect.GetSoundEffect(audioKey);
            _audioManager.PlaySoundEffect(ref audioList._audioClip, audioList._audioVolumeScale);
        }
    }
}
