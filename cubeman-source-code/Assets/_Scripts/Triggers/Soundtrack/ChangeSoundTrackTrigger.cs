using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Trigger
{
    public sealed class ChangeSoundTrackTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private StageSoundTrack playSoundTrack;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                AudioController.Instance.PlaySoundTrack(playSoundTrack);

                gameObject.SetActive(false);
            }
        }
    }
}
