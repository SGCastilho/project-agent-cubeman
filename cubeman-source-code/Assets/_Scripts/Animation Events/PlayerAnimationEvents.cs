using Cubeman.Audio;
using Cubeman.Player;
using Cubeman.Manager;
using Cubeman.Projectile;
using UnityEngine;

namespace Cubeman.AnimationEvents
{
    public sealed class PlayerAnimationEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;

        private AudioController _audioManager;
        private ObjectPoolingManager _poolingManager;

        private ProjectileBehaviour _currentProjectile;

        [Header("Settings")]
        [SerializeField] private Transform shootingPointTransform;

        [Space(12)]

        [SerializeField] private ParticleSystem plasmaMuzzleParticle;

        private string _projectileKey;
        private AudioClip _projectileAudioClip;
        private float _projectileAudioClipVolumeScale;

        private string _ultimateProjectileKey;
        private AudioClip _ultimateAudioClip;
        private float _ultimateAudioClipVolumeScale;

        private void Awake() => CacheComponets();

        private void CacheComponets()
        {
            _audioManager = AudioController.Instance;
            _poolingManager = ObjectPoolingManager.Instance;
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
            var muzzleTransform = plasmaMuzzleParticle.GetComponent<Transform>();

            if (behaviour.Moviment.MoveRight)
            {
                muzzleTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                muzzleTransform.localEulerAngles = new Vector3(0f, 180f, 0f);
            }
        }

        public void ShootEvent()
        {
            InstantiateProjectile(_projectileKey);

            _audioManager.PlaySoundEffect(ref _projectileAudioClip, _projectileAudioClipVolumeScale);
        }

        public void ShootUltimateEvent()
        {
            behaviour.Input.GameplayInputs(true);

            behaviour.Moviment.Gravity.FreezeGravity = false;

            behaviour.Status.InvensibleMode = false;
            behaviour.Status.UltimateReady = false;

            InstantiateProjectile(_ultimateProjectileKey);

            _audioManager.PlaySoundEffect(ref _ultimateAudioClip, _ultimateAudioClipVolumeScale);
        }

        private void InstantiateProjectile(string projectileKey)
        {
            _currentProjectile = _poolingManager.SpawnPrefab(projectileKey, shootingPointTransform.position)
                .GetComponent<ProjectileBehaviour>();

            _currentProjectile.Moviment.MoveRight = behaviour.Moviment.MoveRight;
            _currentProjectile.ResetTimer();
        }

        public void StaggerEvent()
        {
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
