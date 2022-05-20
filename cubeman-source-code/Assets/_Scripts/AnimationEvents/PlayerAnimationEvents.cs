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

        [Header("Settings")]
        [SerializeField] private Transform shootingPointTransform;

        private string _projectileKey;
        private AudioClip _projectileAudioClip;
        private float _projectileAudioClipVolumeScale;

        private string _ultimateProjectileKey;
        private AudioClip _ultimateAudioClip;
        private float _ultimateAudioClipVolumeScale;

        private ObjectPoolingManager _poolingManager;
        private AudioManager _audioManager;

        private ProjectileBehaviour _currentProjectile;

        private void Awake() => CacheComponets();

        private void OnEnable() => GetProjectileData();

        private void CacheComponets()
        {
            _poolingManager = ObjectPoolingManager.Instance;
            _audioManager = AudioManager.Instance;
        }

        private void GetProjectileData()
        {
            _projectileKey = behaviour.Shoot.ProjectileData.Key;
            _projectileAudioClip = behaviour.Shoot.ProjectileData.ProjectileSFX;
            _projectileAudioClipVolumeScale = behaviour.Shoot.ProjectileData.VolumeScale;

            _ultimateProjectileKey = behaviour.Shoot.UltimateData.Key;
            _ultimateAudioClip = behaviour.Shoot.UltimateData.ProjectileSFX;
            _ultimateAudioClipVolumeScale = behaviour.Shoot.UltimateData.VolumeScale;
        }

        public void ShootEvent()
        {
            InstantiateProjectile(_projectileKey);

            _audioManager.PlaySoundEffect(ref _projectileAudioClip, _projectileAudioClipVolumeScale);
        }

        public void ShootUltimateEvent()
        {
            InstantiateProjectile(_ultimateProjectileKey);

            _audioManager.PlaySoundEffect(ref _ultimateAudioClip, _ultimateAudioClipVolumeScale);

            behaviour.Input.GameplayInputs(true);
            behaviour.Moviment.Gravity.FreezeGravity = false;
            behaviour.Status.InvensibleMode = false;
            behaviour.Status.UltimateReady = false;
        }

        private void InstantiateProjectile(string projectileKey)
        {
            _currentProjectile = _poolingManager.SpawnPrefab(projectileKey, shootingPointTransform.position).GetComponent<ProjectileBehaviour>();
            _currentProjectile.Moviment.MoveRight = behaviour.Moviment.MoveRight;
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
