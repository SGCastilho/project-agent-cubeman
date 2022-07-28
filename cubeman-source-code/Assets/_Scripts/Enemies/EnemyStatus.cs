using Cubeman.Audio;
using Cubeman.Interfaces;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EnemyStatus : MonoBehaviour, IDamageble
    {
        public delegate void EnemyDeath();
        public event EnemyDeath OnEnemyDeath;

        public delegate void EnemyEndDeath(string vfxKey, Vector3 posistion);
        public event EnemyEndDeath OnEnemyEndDeath;

        [Header("Classes")]
        [SerializeField] private EnemyDataLoader dataLoader;
        [SerializeField] private LocalAudioController localAudio;
        [SerializeField] private AudioSoundEffects soundEffects;

        [Header("Settings")]
        [SerializeField] private int enemyHealth;

        [Space(12)]

        [SerializeField] private Transform explosionVFXSpawnPoint;
        
        private const string IMPACT_AUDIO_KEY = "audio_impact";
        private const string EXPLOSION_AUDIO_KEY = "audio_explosion";
        private const string EXPLOSION_VFX_KEY = "vfx_small_explosion";

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
                OnEnemyDeath?.Invoke();

                localAudio.PlaySoundEffectInOrder(ref ExplosionSFX);

                OnEnemyEndDeath?.Invoke(EXPLOSION_VFX_KEY, explosionVFXSpawnPoint.position);

                enemyHealth = 0;

                Destroy(disableGameObject);
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

            Destroy(disableGameObject);
        }
    }
}
