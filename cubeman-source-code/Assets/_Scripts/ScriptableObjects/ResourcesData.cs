using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Resources Data", menuName = "Scriptable Object/Resources Data", order = 1)]
    public sealed class ResourcesData : ScriptableObject
    {
        #region Encapsulation
        public int Resources { get => resources; set => AddResource(value); }
        public int Capacitors { get => capacitors; set => AddCapacitors(value); }
        #endregion

        public delegate void AddResources(int amountResources, int currentResources);
        public event AddResources OnAddResources;

        [Header("Settings")]
        [SerializeField] private int resources = 0;
        [SerializeField] private int maxResources = 9999;

        [Space(6)]

        [SerializeField] private int capacitors = 0;

        private void OnEnable() => ResetResources();

        private void AddResource(int amount)
        {
            resources += amount;
            if(resources > maxResources) { resources = maxResources; }

            if(OnAddResources != null) { OnAddResources(amount, resources); }
        }

        private void AddCapacitors(int amount)
        {
            capacitors += amount;
        }

        public void RemoveResources(int amount)
        {
            resources -= amount;

            if (resources < 0)
            {
                resources = 0;
            }
        }

        public void RemoveCapacitors(int amount)
        {
            capacitors -= amount;

            if (capacitors < 0)
            {
                capacitors = 0;
            }
        }

        private void ResetResources() 
        {
            resources = 9000;
            capacitors = 90;
        }
    }
}
