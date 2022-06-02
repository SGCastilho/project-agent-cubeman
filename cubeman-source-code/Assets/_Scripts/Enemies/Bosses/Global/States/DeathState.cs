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

        [SerializeField] private UnityEvent OnBossDeathEnd;

        [Header("Settings")]
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
                OnBossDeathEnd?.Invoke();
            }
        }

        private void BossDeathStart()
        {
            if (!_bossDeathStart)
            {
                var explosionSFX = behaviour.SoundEffects.GetSoundEffect(AUDIO_EXPLOSION_KEY);
                AudioController.Instance.PlaySoundEffectAfterMiliseconds(explosionSFX._audioClip, 
                    explosionSFX._audioVolumeScale, 2400);

                AudioController.Instance.StopSmoothSoundTrack();
                PlayerBehaviour.Instance.Input.GameplayInputs(false);

                behaviour.Movement.IsMoving = false;
                behaviour.Movement.Gravity.FreezeGravity = true;
                behaviour.Animator.IsDeadAnimation = true;
                
                _bossDeathStart = true;
            }
        }
    }
}