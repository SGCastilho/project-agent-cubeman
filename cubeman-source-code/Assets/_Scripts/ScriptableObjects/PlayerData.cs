using UnityEngine;

namespace Cubeman.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Object/Player Data", order = 0)]
    public sealed class PlayerData : ScriptableObject
    {
        #region Encapsulation
        public int Health { get => playerHealth; }
        public int HealthLevel { get => playerHealthLevel; }
        public float HealthScaling { get => playerHealthScaling; }

        public int UltimateCharge { get => playerUltimateCharge; }
        #endregion

        [Header("Settings")]
        [SerializeField] private int playerHealth = 20;
        [SerializeField] private int initPlayerHealth = 20;
        [SerializeField] [Range(1, 5)] private int playerHealthLevel = 1;
        [SerializeField] [Range(0.1f, 1f)] private float playerHealthScaling = 0.6f;

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
                float currentHealth = playerHealth;
                float incrementHealth = currentHealth * playerHealthScaling;
                float newHealth = currentHealth + incrementHealth;

                playerHealth = (int)newHealth;
                playerHealthLevel++;
            }
        }
        
        private void ResetData()
        {
            playerHealth = initPlayerHealth;
            playerHealthLevel = 1;
        }
    }
}
