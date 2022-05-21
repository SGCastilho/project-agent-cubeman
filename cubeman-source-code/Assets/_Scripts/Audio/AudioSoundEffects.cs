using System.Collections.Generic;
using UnityEngine;

namespace Cubeman.Audio
{
    [System.Serializable]
    public struct AudioClipList
    {
        public string _audioKey;
        [Range(0.1f, 1f)] public float _audioVolumeScale;
        public AudioClip _audioClip;
    }

    public sealed class AudioSoundEffects : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private List<AudioClipList> audioClipList;

        public AudioClipList GetSoundEffect(string soundEffectKey)
        {
            var audioClip = new AudioClipList();

            foreach(AudioClipList clip in audioClipList)
            {
                if(clip._audioKey == soundEffectKey)
                {
                    audioClip = clip;
                    break;
                }
            }

            return audioClip;
        }
    }
}
