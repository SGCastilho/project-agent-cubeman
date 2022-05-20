using Cubeman.Interfaces;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyStatus : MonoBehaviour, IDamageble
    {
        public delegate void EnemyDeath();
        public event EnemyDeath OnEnemyDeath;

        [Header("Classes")]
        [SerializeField] private EnemyDataLoader dataLoader;

        [Header("Settings")]
        [SerializeField] private int enemyHealth;

        [Space(6)]

        [SerializeField] private GameObject disableGameObject;

        private void OnEnable() => LoadData();

        private void LoadData()
        {
            enemyHealth = dataLoader.Data.Health;
        }

        public void RecoveryHealth(int recoveryAmount)
        {
            enemyHealth += recoveryAmount;
            if(enemyHealth > dataLoader.Data.Health)
            {
                enemyHealth = dataLoader.Data.Health;
            }
        }

        public void ApplyDamage(int damageAmount)
        {
            enemyHealth -= damageAmount;
            if(enemyHealth <= 0)
            {
                if (OnEnemyDeath != null) { OnEnemyDeath(); }
                enemyHealth = 0;
                disableGameObject.SetActive(false);
            }
        }
    }
}
