using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Visual Effects Data", menuName = "Scriptable Object/Visual Effects")]
    public sealed class VisualEffectsData : ScriptableObject
    {
        #region Encapsulation
        public string Key { get => visualEffectKey; }

        public int Instances { get => prefabInstances; }
        public GameObject Prefab { get => visualEffectPrefab; }
        #endregion

        [Header("Settings")]
        [SerializeField] private string visualEffectKey = "vfx_key";

        [Space(12)]

        [SerializeField] private int prefabInstances = 10;
        [SerializeField] private GameObject visualEffectPrefab;

        #region Editor Variable
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] [Multiline(6)] private string devNote = "Put your dev notes here.";
#endif
        #endregion
    }
}