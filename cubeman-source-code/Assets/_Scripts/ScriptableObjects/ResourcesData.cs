using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Resources Data", menuName = "Scriptable Object/Resources Data", order = 1)]
    public sealed class ResourcesData : ScriptableObject
    {
        #region Encapsulation
        public int Resources { get => resources; set => AddResource(value); }
        #endregion

        public delegate void AddResources(int amountResources, int currentResources);
        public event AddResources OnAddResources;

        [Header("Settings")]
        [SerializeField] private int resources = 0;
        [SerializeField] private int maxResources = 9999;

        private void OnEnable() => ResetResources();

        private void AddResource(int amount)
        {
            resources += amount;
            if(resources > maxResources) { resources = maxResources; }

            if(OnAddResources != null) { OnAddResources(amount, resources); }
        }

        private void ResetResources() 
        {
            resources = 0;
        }
    }
}
