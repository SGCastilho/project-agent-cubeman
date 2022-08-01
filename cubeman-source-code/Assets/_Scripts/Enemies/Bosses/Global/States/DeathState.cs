using Cubeman.UI;
using Cubeman.Audio;
using Cubeman.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Cubeman.Enemies 
{
    public class DeathState : State
    {
        [Header("Classes")]
        [SerializeField] private BossBehaviour behaviour;

        [Header("Unity Events")]

        [Space(12)]

        [SerializeField] private UnityEvent OnBossDeathStart;
        [SerializeField] private UnityEvent OnBossDeathEnd;

        [Header("Settings")]
        [SerializeField] private int bossStartDeathDurationMilliseconds = 2400;
        [SerializeField] [Range(6f, 12f)] private float bossDeathDuration = 8f;
        [SerializeField] [Tooltip("Enable inputs when boss death end.")] private bool enablePlayerInputs;

        private const string AUDIO_EXPLOSION_KEY = "audio_explosion";

        private bool _bossDeathStart;
        private bool _bossFlashCalled;

        private float _currentBossDeathDuration;

        public override State RunCurrentState()
        {
            BossDeathStart();

            BossDeathTimer();

            return this;
        }

        private void BossDeathTimer()
        {
            _currentBossDeathDuration += Time.deltaTime;

            if(!_bossFlashCalled && _currentBossDeathDuration >= bossDeathDuration / 1.2f)
            {
                UIWhiteFlash.Instance.BossDeathFlash();
                _bossFlashCalled = true;
            }

            if (_currentBossDeathDuration >= bossDeathDuration)
            {
                behaviour.gameObject.SetActive(false);

                BossDeathEnd();

                _currentBossDeathDuration = 0;
            }
        }

        private void BossDeathEnd()
        {
            if(enablePlayerInputs)
            {
                PlayerBehaviour.Instance.Input.GameplayInputs(true);
            }
            else
            {
                behaviour.VisualEffects.ExplosionParticle.StopParticle();

                OnBossDeathEnd?.Invoke();
            }
        }

        private void BossDeathStart()
        {
            if (!_bossDeathStart)
            {
                OnBossDeathStart?.Invoke();

                var explosionSFX = behaviour.SoundEffects.GetSoundEffect(AUDIO_EXPLOSION_KEY);
                AudioController.Instance.PlaySoundEffectAfterMiliseconds(explosionSFX._audioClip, 
                    explosionSFX._audioVolumeScale, bossStartDeathDurationMilliseconds);

                var startDeathDurationToFloat = (float)bossStartDeathDurationMilliseconds / 1000;

                behaviour.VisualEffects.ExplosionParticle.PlayParticle(startDeathDurationToFloat);

                AudioController.Instance.StopSmoothSoundTrack();
                PlayerBehaviour.Instance.Input.GameplayInputs(false);

                behaviour.Moviment.IsMoving = false;
                behaviour.Moviment.Gravity.FreezeGravity = true;
                behaviour.Animator.IsDeadAnimation = true;
                
                _bossDeathStart = true;
            }
        }
    }
}