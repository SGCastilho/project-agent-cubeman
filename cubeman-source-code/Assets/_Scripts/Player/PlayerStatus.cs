using System.Collections;
using Cubeman.Interfaces;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Player
{
    public sealed class PlayerStatus : MonoBehaviour, IDamageble
    {
        #region Encapsulation  
        public bool IsDead { get => _isDead; }
        public bool InvensibleMode { set => invensibleMode = value; }
        public bool UltimateReady 
        {
            get => _ultimateReady;
            set 
            {
                if(value == true)
                {
                    currentUltimateCharge = _ultimateCharge;
                    _ultimateReady = true;
                }
                else if(value == false)
                {
                    currentUltimateCharge = 0;
                    _ultimateInCouldown = true;
                    _ultimateReady = false;
                    if(OnPlayerUltimateReset != null) { OnPlayerUltimateReset(); }
                    StartCoroutine(UltimateCouldownCoroutine());
                }
            }
        }

        public string StaggerSFX { get => STAGGER_AUDIO_KEY; }

        internal PlayerData Data { get => data; }
        #endregion

        public delegate void PlayerRecovery(float currentHealth, float maxHealth);
        public event PlayerRecovery OnPlayerRecovery;

        public delegate void PlayerTakeDamage(float currentHealth, float maxHealth);
        public event PlayerTakeDamage OnPlayerTakeDamage;

        public delegate void PlayerUltimateProgress(ref float progress, float maxProgres);
        public event PlayerUltimateProgress OnPlayerUltimateProgress;

        public delegate void PlayerUltimateReset();
        public event PlayerUltimateReset OnPlayerUltimateReset;

        public delegate void PlayerDeathStart();
        public event PlayerDeathStart OnPlayerDeathStart;

        public delegate void PlayerDeath(ref AudioClip clip, float volumeScale);
        public event PlayerDeath OnPlayerDeath;

        public delegate void PlayerDeathComplete();
        public event PlayerDeathComplete OnPlayerDeathComplete;

        [Header("Data")]
        [SerializeField] private PlayerData data;

        [Header("Classes")]
        [SerializeField] private PlayerBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private int currentHealth;
        [SerializeField] [Range(0.1f, 1f)] private float immunityTime = 0.6f;
        [Space(12)]
        [SerializeField] [Range(0.1f, 4f)] private float deathTime = 2f;
        [Space(12)]
        [SerializeField] private bool invensibleMode;
        [Space(12)]
        [SerializeField] private float currentUltimateCharge;
        [SerializeField] [Range(0.1f, 2f)] private float ultimateCouldownDuration = 1f;

        private const string STAGGER_AUDIO_KEY = "audio_stagger";
        private const string DEATH_AUDIO_KEY = "audio_death";

        private bool _damageImmune;
        private bool _isDead;

        private int _ultimateCharge;
        private bool _ultimateReady;
        private bool _ultimateInCouldown;

        private void OnEnable() => LoadData();

        private void LoadData()
        {
            currentHealth = data.Health;
            _ultimateCharge = data.UltimateCharge;
        }

        private void Update() => UltimateCharge();

        private void UltimateCharge()
        {
            if (!_isDead && !_ultimateInCouldown && !_ultimateReady)
            {
                currentUltimateCharge += Time.deltaTime;
                if (currentUltimateCharge >= _ultimateCharge)
                {
                    currentUltimateCharge = _ultimateCharge;
                    _ultimateReady = true;
                }

                if(OnPlayerUltimateProgress != null) 
                { OnPlayerUltimateProgress(ref currentUltimateCharge, data.UltimateCharge); }
            }
        }

        IEnumerator UltimateCouldownCoroutine()
        {
            yield return new WaitForSeconds(ultimateCouldownDuration);
            _ultimateInCouldown = false;
        }

        public void RecoveryHealth(int recovery)
        {
            if(!_isDead)
            {
                currentHealth += recovery;
                if (currentHealth > data.Health)
                {
                    currentHealth = data.Health;
                }

                if (OnPlayerRecovery != null) { OnPlayerRecovery(currentHealth, data.Health); }
            }
        }

        public void RecoveryHealth(float percentage)
        {
            if (!_isDead)
            {
                float heathCalculation = data.Health * percentage;
                currentHealth += (int)heathCalculation;

                if (currentHealth > data.Health)
                {
                    currentHealth = data.Health;
                }

                if (OnPlayerRecovery != null) { OnPlayerRecovery(currentHealth, data.Health); }
            }
        }

        public void ApplyDamage(int damage)
        {
            if(_isDead) return;

            if(!_damageImmune && !invensibleMode)
            {
                currentHealth -= damage;

                behaviour.Moviment.Gravity.FreezeGravity = true;

                if (currentHealth <= 0)
                {
                    _isDead = true;
                    currentHealth = 0;
                    StartCoroutine(DeathCoroutine());
                }
                else
                {
                    behaviour.Animation.TakeDamageAnimation = true;
                    StartCoroutine(ImmunityCoroutine());
                }

                behaviour.VisualEffects.DoFlashHit();

                if(OnPlayerTakeDamage != null) { OnPlayerTakeDamage(currentHealth, data.Health); }
            }
        }

        public void InstaDeath()
        {
            currentHealth = 0;
            if(OnPlayerTakeDamage != null) { OnPlayerTakeDamage(currentHealth, data.Health); }

            _isDead = true;

            StartCoroutine(DeathCoroutine());
        }

        IEnumerator ImmunityCoroutine()
        {
            _damageImmune = true;
            yield return new WaitForSeconds(immunityTime);
            _damageImmune = false;
        }

        IEnumerator DeathCoroutine()
        {
            OnPlayerDeathStart?.Invoke();

            yield return new WaitForSeconds(0.2f);

            behaviour.Input.GameplayInputs(false);
            behaviour.Moviment.Gravity.FreezeGravity = true;
            behaviour.Animation.IsDeadAnimation = true;

            yield return new WaitForSeconds(1f);

            if(OnPlayerDeath != null) 
            {
                var deathAudio = behaviour.SoundEffect.GetSoundEffect(DEATH_AUDIO_KEY);
                OnPlayerDeath(ref deathAudio._audioClip, deathAudio._audioVolumeScale); 
            }
            behaviour.Moviment.GraphicsTransform.gameObject.SetActive(false);

            yield return new WaitForSeconds(deathTime);

            OnPlayerDeathComplete?.Invoke();
        }
    }
}
