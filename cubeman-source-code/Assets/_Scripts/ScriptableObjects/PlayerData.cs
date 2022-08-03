using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Object/Player Data", order = 0)]
    public sealed class PlayerData : ScriptableObject
    {
        #region Encapsulation
        public string UpgradeName { get => upgradeName; }

        public Sprite UpgradePreview { get => upgradePreview; }

        public int Health { get => playerHealth; }
        public int HealthLevel { get => playerHealthLevel; }
        public int HealthScaling { get => playerFixedUpgrade; }

        public int[] AmountToResourcesToUpgrade { get => amountToResourcesToUpgrade; }
        public int[] AmountCapacitorsToUpgrade { get => amountCapacitorsToUpgrade; }

        public int UltimateCharge { get => playerUltimateCharge; }
        #endregion

        [Header("Settings")]
        [SerializeField] private int playerHealth = 20;
        [SerializeField] private int initPlayerHealth = 20;

        [Header("Upgrade Details")]

        [SerializeField] private string upgradeName = "Cubeman Armature";

        [Space(6)]

        [SerializeField] private Sprite upgradePreview;

        [Space(6)]

        [SerializeField] [Range(1, 5)] private int playerHealthLevel = 1;
        [SerializeField] private int playerFixedUpgrade;

        [Space(6)]

        [SerializeField] private int[] amountToResourcesToUpgrade;
        [SerializeField] private int[] amountCapacitorsToUpgrade;

        [Space(12)]

        [SerializeField] private int playerUltimateCharge = 20;

        #region Editor Variable
#if UNITY_EDITOR
        [Space(12)]

        [SerializeField] [Multiline(4)] private string devNotes = "Put your dev notes here.";
#endif
        #endregion

        private void OnEnable() => ResetData();

        public void UpgradeHealth()
        {
            if(playerHealthLevel <= 4)
            {
                int currentHealth = playerHealth;
                int newHealth = currentHealth + playerFixedUpgrade;

                playerHealth = newHealth;
                playerHealthLevel++;
            }
        }

        public int GetNextUpgrade()
        {
            var nextUpgrade = 0;

            int currentHealth = playerHealth;
            int newHealth = currentHealth + playerFixedUpgrade;

            nextUpgrade = newHealth;

            return nextUpgrade;
        }

        private void ResetData()
        {
            playerHealth = initPlayerHealth;
            playerHealthLevel = 1;
        }
    }
}
