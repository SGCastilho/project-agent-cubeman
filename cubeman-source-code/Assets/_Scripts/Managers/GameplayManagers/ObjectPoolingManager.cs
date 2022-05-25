using System.Collections.Generic;
using UnityEngine;

namespace Cubeman.Manager
{
    public class ObjectPoolingManager : MonoBehaviour
    {
        #region Singleton
        public static ObjectPoolingManager Instance;
        #endregion

        [System.Serializable]
        internal class Pool
        {
            internal Pool(string key, int size, GameObject prefab)
            {
                poolKey = key;
                poolSize = size;
                poolPrefab = prefab;
            }

            #region Encapsulation
            internal string Key { get => poolKey; }
            internal int Size { get => poolSize; set => poolSize = value; }
            internal GameObject Prefab { get => poolPrefab; }
            #endregion

            [SerializeField] private string poolKey;
            [SerializeField] private int poolSize;
            [SerializeField] private GameObject poolPrefab;
        }

        [SerializeField] private List<Pool> poolingList;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        private void Awake() => Instance = this;

        public void AddPool(string key, int size, GameObject prefab)
        {
            if(key != null && size > 0 && prefab != null)
            {
                var pool = new Pool(key, size, prefab);
                poolingList.Add(pool);
            }
            else { Debug.LogWarning("Não foi possivel criar uma nova Pool."); }
        }

        public void InitializePooling()
        {
            if(poolingList.Capacity > 0)
            {
                _poolDictionary = new Dictionary<string, Queue<GameObject>>();

                foreach(Pool pool in poolingList)
                {
                    var poolQueue = new Queue<GameObject>();

                    for(int i = 0; i < pool.Size; i++)
                    {
                        var prefab = Instantiate(pool.Prefab);
                        prefab.transform.position = Vector3.zero;
                        prefab.SetActive(false);

                        poolQueue.Enqueue(prefab);
                    }

                    if(_poolDictionary.ContainsKey(pool.Key))
                    {
                        for(int i = 0; i < pool.Size; i++)
                        {
                            var currentObject = poolQueue.Dequeue();
                            _poolDictionary[pool.Key].Enqueue(currentObject);
                        }
                    }
                    else
                    {
                        _poolDictionary.Add(pool.Key, poolQueue);
                    }
                }
            }
            else { Debug.LogWarning("Não há Pools a serem instanciadas."); }

            poolingList.Clear();
        }

        public GameObject SpawnPrefab(string key)
        {
            if(_poolDictionary.ContainsKey(key))
            {
                var prefab = _poolDictionary[key].Dequeue();
                prefab.SetActive(true);
                _poolDictionary[key].Enqueue(prefab);

                return prefab;
            }

            return null;
        }

        public GameObject SpawnPrefab(string key, Vector3 posistion)
        {
            if (_poolDictionary.ContainsKey(key))
            {
                var prefab = _poolDictionary[key].Dequeue();
                prefab.transform.position = posistion;
                prefab.SetActive(true);
                _poolDictionary[key].Enqueue(prefab);

                return prefab;
            }

            return null;
        }

        public GameObject SpawnPrefab(string key, Vector3 posistion, Transform parent)
        {
            if (_poolDictionary.ContainsKey(key))
            {
                var prefab = _poolDictionary[key].Dequeue();
                prefab.transform.SetParent(parent);
                prefab.transform.localPosition = posistion;
                prefab.SetActive(true);
                _poolDictionary[key].Enqueue(prefab);

                return prefab;
            }

            return null;
        }

        public void SpawnPrefabNoReturn(string key, Vector3 posistion)
        {
            if (_poolDictionary.ContainsKey(key))
            {
                var prefab = _poolDictionary[key].Dequeue();
                prefab.transform.position = posistion;
                prefab.SetActive(true);
                _poolDictionary[key].Enqueue(prefab);
            }
        }
    }
}
