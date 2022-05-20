using UnityEngine;

namespace Cubeman.Audio
{
    public sealed class AudioStageSoundTrack : MonoBehaviour
    {
        #region Encapsulation
        public AudioClip Stage { get => stageAudioClip; }
        public AudioClip Encounter { get => encounterAudioClip; }
        public AudioClip BossFight { get => bossFightAudioClip; }
        #endregion

        [Header("Settings")]
        [SerializeField] private AudioClip stageAudioClip;
        [SerializeField] private AudioClip encounterAudioClip;
        [SerializeField] private AudioClip bossFightAudioClip;
    }
}
