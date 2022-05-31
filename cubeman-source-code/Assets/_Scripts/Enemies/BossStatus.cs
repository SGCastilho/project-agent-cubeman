using Cubeman.Audio;
using Cubeman.Player;
using Cubeman.Interfaces;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossStatus : MonoBehaviour, IDamageble
    {
        #region Encapsulation
        internal bool InvensibleMode { set => _invensibleMode = value; }
        #endregion

        public delegate void HealthRecovery(float currentHealth, float maxHealth);
        public event HealthRecovery OnRecoveryHealth;

        public delegate void DamageHealth(float currentHealth, float maxHealth);
        public event DamageHealth OnDamageHealth;

        [Header("Classes")]
        [SerializeField] private BossBehaviour behaviour;

        private PlayerBehaviour player;

        [Space(12)]

        [SerializeField] private BossDataLoader dataLoader;
        [SerializeField] private AudioSoundEffects soundEffects;

        private AudioController audioController;

        [Header("Settings")]
        [SerializeField] private int bossHealth;

        private const string IMPACT_AUDIO_KEY = "audio_impact";

        private bool _invensibleMode = true;
        private AudioClipList _impactAudioList;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            audioController = AudioController.Instance;

            player = FindObjectOfType<PlayerBehaviour>();

            bossHealth = dataLoader.Data.Health;

            var impactSFX = soundEffects.GetSoundEffect(IMPACT_AUDIO_KEY);
            _impactAudioList = impactSFX;
        }

        public void ApplyDamage(int damageAmount)
        {
            if(!_invensibleMode)
            {
                bossHealth -= damageAmount;
                if(!player.Status.IsDead && bossHealth <= 0)
                {
                    bossHealth = 0;
                    player.Status.InvensibleMode = true;
                    behaviour.Sequencer.CallDeathState();
                }

                OnDamageHealth?.Invoke(bossHealth, dataLoader.Data.Health);

                audioController.PlaySoundEffectInOrder(ref _impactAudioList);
            }
        }

        public void InstaDeath()
        {
            bossHealth = 0;
            behaviour.Sequencer.CallDeathState();

            OnDamageHealth?.Invoke(bossHealth, dataLoader.Data.Health);
        }

        public void RecoveryHealth(int recoveryAmount)
        {
            bossHealth += recoveryAmount;
            if(bossHealth > dataLoader.Data.Health)
            {
                bossHealth = dataLoader.Data.Health;
            }

            OnRecoveryHealth?.Invoke(bossHealth, dataLoader.Data.Health);
        }
    }
}