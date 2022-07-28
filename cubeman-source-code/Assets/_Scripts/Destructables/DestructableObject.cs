using Cubeman.Audio;
using Cubeman.Manager;
using Cubeman.Interfaces;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Destructable
{
    public sealed class DestructableObject : MonoBehaviour, IDamageble
    {
        public delegate void DestructObject();
        public event DestructObject OnDestructObject;

        [Header("Data")]
        [SerializeField] private DestructableData data;

        [Header("Classes")]
        [SerializeField] private DestructableBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] private Transform vfxSpawnPoint;

        private const string IMPACT_SFX = "audio_impact";
        private const string EXPLOSION_SFX = "audio_explosion";
        private const string EXPLOSION_VFX = "vfx_small_explosion";

        private int _health;

        private AudioClip _impactSFX;
        private float _impactVolumeScale;

        private AudioClipList _explosionAudioList;

        private void OnEnable() => ResetHealth();

        private void ResetHealth()
        {
            _health = data.Health;
        }

        private void Start() => LoadSoundEffects();

        private void LoadSoundEffects()
        {
            var impactAudioList = behaviour.SoundEffects.GetSoundEffect(IMPACT_SFX);
            _impactSFX = impactAudioList._audioClip;
            _impactVolumeScale = impactAudioList._audioVolumeScale;

            var explosionAudioList = behaviour.SoundEffects.GetSoundEffect(EXPLOSION_SFX);
            _explosionAudioList = explosionAudioList;
        }

        public void ApplyDamage(int damageAmount)
        {
            _health -= damageAmount;
            if(_health <= 0)
            {
                ObjectPoolingManager.Instance.SpawnPrefabNoReturn(EXPLOSION_VFX, vfxSpawnPoint.position);

                if (OnDestructObject != null) { OnDestructObject(); }

                _health = 0;

                Destroy(gameObject);

                behaviour.AudioManager.PlaySoundEffectInOrder(ref _explosionAudioList);

                return;
            }

            behaviour.AudioManager.PlaySoundEffect(_impactSFX, _impactVolumeScale);
        }

        public void InstaDeath()
        {
            _health = 0;

            if (OnDestructObject != null) { OnDestructObject(); }

            Destroy(gameObject);

            behaviour.AudioManager.PlaySoundEffectInOrder(ref _explosionAudioList);
        }

        public void RecoveryHealth(int recoveryAmount)
        {
            _health += recoveryAmount;
            if(_health > data.Health)
            {
                _health = data.Health;
            }
        }
    }
}
