using Cubeman.Audio;
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
        [SerializeField] private LocalAudioController localAudio;
        [SerializeField] private AudioSoundEffects soundEffects;

        [Header("Settings")]
        [SerializeField] private int enemyHealth;
        
        private const string IMPACT_AUDIO_KEY = "audio_impact";
        private const string EXPLOSION_AUDIO_KEY = "audio_explosion";

        private AudioClipList ImpactSFX;
        private AudioClipList ExplosionSFX;

        [Space(6)]

        [SerializeField] private GameObject disableGameObject;

        private void OnEnable() => LoadData();

        private void LoadData()
        {
            enemyHealth = dataLoader.Data.Health;
            LoadSoundEffects();
        }

        private void LoadSoundEffects()
        {
            ImpactSFX = soundEffects.GetSoundEffect(IMPACT_AUDIO_KEY);
            ExplosionSFX = soundEffects.GetSoundEffect(EXPLOSION_AUDIO_KEY);
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
                localAudio.PlaySoundEffectInOrder(ref ExplosionSFX);
                enemyHealth = 0;
                disableGameObject.SetActive(false);
            }
            else 
            { 
                localAudio.PlaySoundEffectInOrder(ref ImpactSFX); 
            }
        }

        public void InstaDeath()
        {
            enemyHealth = 0;

            if (OnEnemyDeath != null) { OnEnemyDeath(); }
            
            localAudio.PlaySoundEffectInOrder(ref ExplosionSFX);
            disableGameObject.SetActive(false);
        }
    }
}
