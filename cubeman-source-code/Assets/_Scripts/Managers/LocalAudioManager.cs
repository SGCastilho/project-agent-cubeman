using Cubeman.Audio;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Cubeman.Manager
{
    internal struct SequenceSoundEffect
    {
        internal SequenceSoundEffect(AudioClip clip, float volumeScale)
        {
            _audioClip = clip;
            _volumeScale = volumeScale;
        }

        internal float VolumeScale { get => _volumeScale; }
        internal AudioClip AudioClip { get => _audioClip; }

        private float _volumeScale;
        private AudioClip _audioClip;
    }

    public sealed class LocalAudioManager : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private AudioSource audioSource;

        [Header("Settings")]
        [SerializeField] [Range(100, 400)] private int milisecondsBetweenSoundEffect = 200;
        private bool _queuePlaying;

        private Queue<SequenceSoundEffect> soundEffectQueue = new Queue<SequenceSoundEffect>();

        public void PlaySoundTrack(ref AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;

            if(audioSource.isPlaying) { audioSource.Stop(); }

            audioSource.Play();
        }

        public void PlaySoundTrack(ref AudioClip clip, float volume)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = true;

            if (audioSource.isPlaying) { audioSource.Stop(); }

            audioSource.Play();
        }

        public void PlaySoundEffect(ref AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        public void PlaySoundEffect(AudioClip clip, float volumeScale)
        {
            audioSource.PlayOneShot(clip, volumeScale);
        }

        public void PlaySoundEffectInOrder(ref AudioClipList audioClipList) 
        {
            var sequenceClip = new SequenceSoundEffect(audioClipList._audioClip, audioClipList._audioVolumeScale);
            soundEffectQueue.Enqueue(sequenceClip);
            
            if(!_queuePlaying)
            {
                PlayerSoundEffectInQueue();
                _queuePlaying = true;
            }
        }

        private async void PlayerSoundEffectInQueue()
        {
            while(soundEffectQueue.Count > 0)
            {
                var sequenceClip = soundEffectQueue.Dequeue();
                PlaySoundEffect(sequenceClip.AudioClip, sequenceClip.VolumeScale);
                await Task.Delay(milisecondsBetweenSoundEffect);
            }

            _queuePlaying = false;
        }
    }
}
