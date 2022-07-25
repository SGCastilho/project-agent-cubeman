using UnityEngine;

namespace Cubeman.Audio
{
    public sealed class PlaySingleAudio : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private AudioSource audioSource;

        [Header("Settings")]
        [SerializeField] private AudioClip audioClip;
        [SerializeField] [Range(0.1f, 1f)] private float audioClipVolumeScale = 1f;

        public void PlaySound()
        {
            if (audioSource == null) return;

            audioSource.PlayOneShot(audioClip, audioClipVolumeScale);
        }
    }
}
