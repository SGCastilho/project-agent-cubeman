using Cubeman.Manager;
using Cubeman.Utilities;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Damagable
{
    public sealed class DamagableCollectableSpawn : MonoBehaviour
    {
        private ObjectPoolingManager _poolingManager;

        [Header("Settings")]
        [SerializeField] private CollectableData[] collectableList;
        [SerializeField] private Transform collectableSpawnPoint;

        [Space(12)]

        [SerializeField] [Range(1, 6)] private int minCollectableSpawn = 2;
        [SerializeField] [Range(1, 6)] private int maxCollectableSpawn = 4;

        private int _collectablesToSpawn;
        private int _randomCollectable;
        private int _spawnChange;
        private Vector3 _lastSpawnSide;

        private void Awake() => _poolingManager = ObjectPoolingManager.Instance;

        public void SpawnCollectable()
        {
            if(collectableList.Length > 0)
            {
                RandomizeCollectable();
            }
        }

        private void RandomizeCollectable()
        {
            _collectablesToSpawn = Random.Range(minCollectableSpawn, maxCollectableSpawn);

            for (int i = 0; i < _collectablesToSpawn; i++)
            {
                _randomCollectable = Random.Range(0, collectableList.Length);
                _spawnChange = Random.Range(0, 100);

                if (_spawnChange <= collectableList[_randomCollectable].SpawnChance)
                {
                    var collectablePrefab = _poolingManager.SpawnPrefab(collectableList[_randomCollectable].Key, 
                        collectableSpawnPoint.position).GetComponent<ImpulseObjectOnEnable>();

                    _lastSpawnSide = CheckLastSpawnSide();

                    collectablePrefab.XImpulseSide = _lastSpawnSide;
                    collectablePrefab.ImpulseObject();
                }
            }
        }

        private Vector3 CheckLastSpawnSide()
        {
            if (_lastSpawnSide == Vector3.right) { return Vector3.left; }
            else if (_lastSpawnSide == Vector3.left) { return Vector3.right; }

            return Vector3.right;
        }
    }
}
