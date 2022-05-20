using Cubeman.Audio;
using Cubeman.Manager;
using Cubeman.Player;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class CollectableHeathTankTrigger : MonoBehaviour
    {
        private const string COLLECT_SFX = "audio_collect";

        [Header("Data")]
        [SerializeField] private CollectableHeathTankData collectableData;

        [Header("Classes")]
        [SerializeField] private AudioSoundEffects soundEffects;

        private PlayerBehaviour _player;

        private void Awake() => _player = FindObjectOfType<PlayerBehaviour>();

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _player.Status.RecoveryHealth(collectableData.RecoveryPercentage);

                var audioList = soundEffects.GetSoundEffect(COLLECT_SFX);
                AudioManager.Instance.PlaySoundEffectInOrder(ref audioList);

                gameObject.SetActive(false);
            }
        }
    }
}
