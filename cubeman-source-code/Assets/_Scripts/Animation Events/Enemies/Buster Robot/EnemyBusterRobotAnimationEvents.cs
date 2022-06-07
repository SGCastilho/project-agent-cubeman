using Cubeman.Audio;
using Cubeman.Enemies;
using Cubeman.Manager;
using Cubeman.Projectile;
using UnityEngine;

namespace Cubeman.AnimationEvents
{
    public sealed class EnemyBusterRobotAnimationEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private EnemyBusterRobotBehaviour behaviour;

        private AudioController _audioController;
        private ObjectPoolingManager _poolingManager;

        private ProjectileBehaviour _currentProjectile;

        [Header("Settings")]
        [SerializeField] private Transform shootingPointTransform;

        private string _projectileKey;

        private void Awake() => CacheComponets();

        private void CacheComponets()
        {
            _poolingManager = ObjectPoolingManager.Instance;
            _audioController = AudioController.Instance;
        }

        private void Start() => GetProjectileData();

        private void GetProjectileData()
        {
            _projectileKey = behaviour.DataLoader.Data.Projectile.Key;
        }

        public void ShootEvent()
        {
            InstantiateProjectile(_projectileKey);

            _audioController.PlaySoundEffectInOrder(ref behaviour.shootSFX);
        }

        private void InstantiateProjectile(string projectileKey)
        {
            _currentProjectile = _poolingManager.SpawnPrefab(projectileKey, shootingPointTransform.position)
                .GetComponent<ProjectileBehaviour>();

            _currentProjectile.Moviment.MoveRight = behaviour.Moviment.MoveRight;
            _currentProjectile.ResetTimer();
        }
    }
}
