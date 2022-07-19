using Cubeman.Audio;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class PrologueCutsceneAnimations : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private LocalAudioController audioController;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float radioMusicFadeOutDuration = 0.6f;

        public void StopRadioMusicEvent()
        {
            audioController.StopSoundTrack(radioMusicFadeOutDuration);
        }
    }
}
