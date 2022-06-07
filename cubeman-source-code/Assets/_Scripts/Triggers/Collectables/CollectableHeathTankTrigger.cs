using Cubeman.Audio;
using Cubeman.Player;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class CollectableHeathTankTrigger : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private CollectableHeathTankData collectableData;

        [Header("Classes")]
        [SerializeField] private AudioSoundEffects soundEffects;
        private PlayerBehaviour _player;

        private const string COLLECT_SFX = "audio_collect";

        private void Awake() => CacheComponets();

        private void CacheComponets()
        {
            _player = PlayerBehaviour.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _player.Status.RecoveryHealth(collectableData.RecoveryPercentage);

                var audioList = soundEffects.GetSoundEffect(COLLECT_SFX);
                AudioController.Instance.PlaySoundEffectInOrder(ref audioList);

                gameObject.SetActive(false);
            }
        }
    }
}
