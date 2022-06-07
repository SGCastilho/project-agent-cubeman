using Cubeman.Audio;
using Cubeman.Interfaces;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Destructable
{
    public sealed class DestructableObject : MonoBehaviour, IDamageble
    {
        public delegate void ImpactObject(AudioClip clip, float volumeScale);
        public event ImpactObject OnImpactObject;

        public delegate void DestructObject();
        public event DestructObject OnDestructObject;

        public delegate void DestructObjectFinish(ref AudioClipList audioClipList);
        public event DestructObjectFinish OnDestructObjectFinish;

        [Header("Data")]
        [SerializeField] private DestructableData data;

        [Header("Classes")]
        [SerializeField] private DestructableBehaviour behaviour;

        private int _health;

        private const string IMPACT_SFX = "audio_impact";
        private const string EXPLOSION_SFX = "audio_explosion";

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
                if (OnDestructObject != null) { OnDestructObject(); }

                _health = 0;
                gameObject.SetActive(false);

                if(OnDestructObjectFinish != null) { OnDestructObjectFinish(ref _explosionAudioList); }
            }
            else 
            {
                if(OnImpactObject != null) { OnImpactObject(_impactSFX, _impactVolumeScale); }
            }
        }

        public void InstaDeath()
        {
            _health = 0;

            if (OnDestructObject != null) { OnDestructObject(); }

            gameObject.SetActive(false);

            if(OnDestructObjectFinish != null) { OnDestructObjectFinish(ref _explosionAudioList); }
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
