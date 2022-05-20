using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    public class CollectableData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => collectableKey; }
        public GameObject Prefab { get => collectablePreafab; }
        public int Instances { get => collectableInstances; }

        public int SpawnChance { get => collectableSpawnChance; }
        #endregion

        [Header("Settings")]
        [SerializeField] protected string collectableKey = "collectable_key";
        [SerializeField] protected GameObject collectablePreafab;
        [SerializeField] [Range(1, 20)] protected int collectableInstances = 1;

        [Space(12)]

        [SerializeField] [Range(10, 100)] protected int collectableSpawnChance = 60;

        #region Editor
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] [Multiline(4)] private string devNotes = "Default Collectable.";
#endif
        #endregion
    }
}
