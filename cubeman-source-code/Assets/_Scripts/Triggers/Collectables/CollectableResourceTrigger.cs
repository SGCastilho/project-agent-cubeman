using Cubeman.Audio;
using Cubeman.ScriptableObjects;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class CollectableResourceTrigger : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private ResourcesData resourcesData;
        [SerializeField] private CollectableResourceData collectableData;

        [Header("Classes")]
        [SerializeField] private AudioSoundEffects soundEffects;

        private const string COLLECT_SFX = "audio_collect";

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                resourcesData.Resources = collectableData.Resources;

                var audioList = soundEffects.GetSoundEffect(COLLECT_SFX);
                AudioController.Instance.PlaySoundEffectInOrder(ref audioList);

                gameObject.SetActive(false);
            }
        }
    }
}
