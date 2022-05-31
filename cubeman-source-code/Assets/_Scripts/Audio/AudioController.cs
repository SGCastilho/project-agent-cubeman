using DG.Tweening;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Cubeman.Audio
{
    public enum StageSoundTrack { STAGE, ENCOUNTER, BOSS_FIGHT }

    public sealed class AudioController : MonoBehaviour
    {
        #region Singleton
        public static AudioController Instance;
        #endregion

        [Header("Classes")]
        [SerializeField] private AudioStageSoundTrack stageSoundTrack;

        [Space(12)]

        [SerializeField] private AudioSource soundTrackAudioSource;
        [SerializeField] private AudioSource soundEffectAudioSource;

        [Header("Settings")]
        [SerializeField] [Range(0.1f, 1f)] private float soundTrackVolume = 0.6f;
        [SerializeField] [Range(0.1f, 1f)] private float soundEffectVolume = 1f;

        [Space(12)]

        [SerializeField] [Range(0.1f, 2f)] private float soundTrackFadeIn = 1f;
        [SerializeField] [Range(0.1f, 2f)] private float soundTrackFadeOut = 1f;

        [Space(12)]

        [SerializeField] [Range(100, 400)] private int milisecondsBetweenSoundEffect = 200;

        private Queue<SequenceSoundEffect> _soundEffectQueue = new Queue<SequenceSoundEffect>();
        private bool _queuePlaying;
        private float _currentVolumeScale;
        private AudioClip _currentAudioClip;

        private void Awake() => Instance = this;

        private void OnEnable() => SetAudioSourcesVolume();

        private void Start() => PlaySoundTrack(StageSoundTrack.STAGE);

        private void SetAudioSourcesVolume()
        {
            soundTrackAudioSource.volume = soundTrackVolume;
            soundEffectAudioSource.volume = soundEffectVolume;
        }

        public void PlaySoundTrack(StageSoundTrack selectedSoundTrack)
        {
            AudioClip soundTrackAudioClip = null;

            switch (selectedSoundTrack)
            {
                case StageSoundTrack.STAGE:
                    soundTrackAudioClip = stageSoundTrack.Stage;
                    break;
                case StageSoundTrack.ENCOUNTER:
                    soundTrackAudioClip = stageSoundTrack.Encounter;
                    break;
                case StageSoundTrack.BOSS_FIGHT:
                    soundTrackAudioClip = stageSoundTrack.BossFight;
                    break;
            }

            soundTrackAudioSource.clip = soundTrackAudioClip;

            if(soundTrackAudioSource.isPlaying) { soundTrackAudioSource.Stop(); }

            soundTrackAudioSource.Play();
        }

        public void StopSoundTrack()
        {
            soundEffectAudioSource.Stop();
        }

        public async void PlaySmoothSoundTrack(StageSoundTrack selectedSoundTrack)
        {
            AudioClip soundTrackAudioClip = null;

            switch(selectedSoundTrack)
            {
                case StageSoundTrack.STAGE:
                    soundTrackAudioClip = stageSoundTrack.Stage;
                    break;
                case StageSoundTrack.ENCOUNTER:
                    soundTrackAudioClip = stageSoundTrack.Encounter;
                    break;
                case StageSoundTrack.BOSS_FIGHT:
                    soundTrackAudioClip = stageSoundTrack.BossFight;
                    break;
            }

            await CheckIfSoundTrackIsPlaying();

            soundTrackAudioSource.clip = soundTrackAudioClip;

            await FadeIn(soundTrackAudioSource, soundTrackVolume);

            soundTrackAudioSource.Play();
        }

        public async void StopSmoothSoundTrack()
        {
            await FadeOut(soundTrackAudioSource);

            soundTrackAudioSource.Stop();
        }

        private async Task CheckIfSoundTrackIsPlaying()
        {
            if (soundTrackAudioSource.isPlaying)
            {
                await FadeOut(soundTrackAudioSource);
            }

            await Task.Yield();
        }

        private async Task FadeIn(AudioSource audioSource)
        {
            audioSource.DOKill();
            audioSource.DOFade(1f, soundTrackFadeIn);

            var taskDelay = (int)soundTrackFadeIn * 1000;

            await Task.Delay(taskDelay);
        }

        private async Task FadeIn(AudioSource audioSource, float maxVolume)
        {
            audioSource.DOKill();
            audioSource.DOFade(maxVolume, soundTrackFadeIn);

            var taskDelay = (int)soundTrackFadeIn * 1000;

            await Task.Delay(taskDelay);
        }

        private async Task FadeOut(AudioSource audioSource)
        {
            audioSource.DOKill();
            audioSource.DOFade(0f, soundTrackFadeOut);

            var taskDelay = (int)soundTrackFadeOut * 1000;

            await Task.Delay(taskDelay);
        }

        public void PlaySoundEffect(ref AudioClip clip)
        {
            soundEffectAudioSource.PlayOneShot(clip);
        }

        public void PlaySoundEffect(ref AudioClip clip, float volumeScale)
        {
            soundEffectAudioSource.PlayOneShot(clip, volumeScale);
        }

        public async void PlaySoundEffectAfterMiliseconds(AudioClip clip, int miliseconds)
        {
            await Task.Delay(miliseconds);
            soundEffectAudioSource.PlayOneShot(clip);
        }

        public async void PlaySoundEffectAfterMiliseconds(AudioClip clip, float volumeScale, int miliseconds)
        {
            await Task.Delay(miliseconds);
            soundEffectAudioSource.PlayOneShot(clip, volumeScale);
        }

        public void PlaySoundEffectInOrder(ref AudioClipList audioClipList)
        {
            var sequenceClip = new SequenceSoundEffect(audioClipList._audioClip, audioClipList._audioVolumeScale);
            _soundEffectQueue.Enqueue(sequenceClip);

            if (!_queuePlaying)
            {
                PlayerSoundEffectInQueue();
                _queuePlaying = true;
            }
        }

        private async void PlayerSoundEffectInQueue()
        {
            while (_soundEffectQueue.Count > 0)
            {
                var sequence = _soundEffectQueue.Dequeue();
                _currentAudioClip = sequence.AudioClip;
                _currentVolumeScale = sequence.VolumeScale;
                PlaySoundEffect(ref _currentAudioClip, _currentVolumeScale);
                await Task.Delay(milisecondsBetweenSoundEffect);
            }

            _queuePlaying = false;
        }
    }
}
