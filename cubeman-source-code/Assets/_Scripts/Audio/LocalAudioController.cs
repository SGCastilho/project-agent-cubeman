using DG.Tweening;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Cubeman.Audio
{
    public struct SequenceSoundEffect
    {
        public SequenceSoundEffect(AudioClip clip, float volumeScale)
        {
            _audioClip = clip;
            _volumeScale = volumeScale;
        }

        #region Encapsulation
        public float VolumeScale { get => _volumeScale; }
        public AudioClip AudioClip { get => _audioClip; }
        #endregion

        private float _volumeScale;
        private AudioClip _audioClip;
    }

    public sealed class LocalAudioController : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private AudioSource audioSource;

        [Header("Settings")]
        [SerializeField] [Range(100, 400)] private int milisecondsBetweenSoundEffect = 200;

        private Queue<SequenceSoundEffect> soundEffectQueue = new Queue<SequenceSoundEffect>();

        private bool _queuePlaying;
        private float _currentVolume;

        private void OnEnable() => SetupObject();

        private void SetupObject()
        {
            _currentVolume = audioSource.volume;
        }

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

        public void StopSoundTrack()
        {
            audioSource.Stop();

            audioSource.clip = null;
            audioSource.volume = _currentVolume;
        }

        public void StopSoundTrack(float volumeFadeOutDuration)
        {
            _currentVolume = audioSource.volume;

            audioSource.DOFade(0f, volumeFadeOutDuration).OnComplete(StopSoundTrack);
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
                PlaySoundEffectInQueue();
                _queuePlaying = true;
            }
        }

        private async void PlaySoundEffectInQueue()
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
