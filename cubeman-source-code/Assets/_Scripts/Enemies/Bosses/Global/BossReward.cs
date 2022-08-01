using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossReward : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private ResourcesData resourcesData;

        [Header("Settings")]
        [SerializeField] private int resourcesReward;

        private bool _rewardGived;

        public void GiveBossReward()
        {
            if (_rewardGived) return;

            resourcesData.Resources = resourcesReward;

            _rewardGived = true;
        }
    }
}
