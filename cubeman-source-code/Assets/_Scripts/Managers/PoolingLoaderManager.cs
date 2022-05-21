using Cubeman.Player;
using Cubeman.Enemies;
using Cubeman.ScriptableObjects;
using System.Threading.Tasks;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class PoolingLoaderManager : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private ObjectPoolingManager manager;

        [Header("Settings")]
        [SerializeField] private CollectableData[] collectableToSpawn;

        private PlayerBehaviour _player;

        private void Awake() => CacheComponents();

        private void CacheComponents()
        {
            _player = FindObjectOfType<PlayerBehaviour>();
        }

        private void OnEnable() => SpawnPoolings();

        private async void SpawnPoolings()
        {
            await SetProjectiles();
            await SetCollectables();

            manager.InitializePooling();
        }

        private async Task SetProjectiles()
        {
            await SetPlayerProjectiles();
            await SetEnemiesProjectiles();
        }

        private async Task SetPlayerProjectiles()
        {
            var playerProjectile = new ProjectileData[2];
            playerProjectile[0] = _player.Shoot.ProjectileData;
            playerProjectile[1] = _player.Shoot.UltimateData;

            for (int i = 0; i < playerProjectile.Length; i++)
            {
                manager.AddPool(playerProjectile[i].Key, playerProjectile[i].Instances, playerProjectile[i].Prefab);
            }

            await Task.Yield();
        }

        private async Task SetEnemiesProjectiles()
        {
            var projectileEnemies = FindObjectsOfType<EnemyDataLoader>();

            if(projectileEnemies != null)
            {
                for (int i = 0; i < projectileEnemies.Length; i++)
                {
                    if(projectileEnemies[i].Data.Projectile != null)
                    {
                        manager.AddPool(projectileEnemies[i].Data.Projectile.Key,
                            projectileEnemies[i].Data.Projectile.Instances, projectileEnemies[i].Data.Projectile.Prefab);
                    }
                }
            }

            await Task.Yield();
        }

        private async Task SetCollectables()
        {
            if(collectableToSpawn.Length > 0)
            {
                for(int i = 0; i < collectableToSpawn.Length; i++)
                {
                    manager.AddPool(collectableToSpawn[i].Key, collectableToSpawn[i].Instances, 
                        collectableToSpawn[i].Prefab);
                }
            }

            await Task.Yield();
        }
    }
}
