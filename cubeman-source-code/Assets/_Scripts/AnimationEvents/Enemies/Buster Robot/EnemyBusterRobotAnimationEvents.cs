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

        [Header("Settings")]
        [SerializeField] private Transform shootingPointTransform;

        private string _projectileKey;

        private ObjectPoolingManager _poolingManager;
        private ProjectileBehaviour _currentProjectile;

        private void Awake() => CacheComponets();

        private void Start() => GetProjectileData();

        private void CacheComponets()
        {
            _poolingManager = ObjectPoolingManager.Instance;
        }

        private void GetProjectileData()
        {
            _projectileKey = behaviour.DataLoader.Data.Projectile.Key;
        }

        public void ShootEvent()
        {
            InstantiateProjectile(_projectileKey);
        }

        private void InstantiateProjectile(string projectileKey)
        {
            _currentProjectile = _poolingManager.SpawnPrefab(projectileKey, shootingPointTransform.position).GetComponent<ProjectileBehaviour>();
            _currentProjectile.Moviment.MoveRight = behaviour.Moviment.MoveRight;
        }
    }
}
