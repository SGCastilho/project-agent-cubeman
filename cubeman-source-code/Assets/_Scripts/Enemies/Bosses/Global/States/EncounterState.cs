using Cubeman.Audio;
using Cubeman.Player;
using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class EncounterState : State
    {
        [Header("Classes")]
        [SerializeField] private BossBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] [Range(0f, 1f)] private float encounterDelay = 1f;
        [SerializeField] [Range(0.1f, 4f)] private float encounterDuration = 2f;

        [Space(12)]

        [SerializeField] private State nextState;

        private bool _encounterDelayFinish;
        private float _currentEncounterDelay;
        private float _currentEncounterDuration;

        public override State RunCurrentState()
        {
            EncounterDelayTimer();

            if(_encounterDelayFinish)
            {
                _currentEncounterDuration += Time.deltaTime;
                if(_currentEncounterDuration >= encounterDuration)
                {
                    behaviour.Animator.EncounterAnimation = false;
                    _currentEncounterDuration = 0;
                    _encounterDelayFinish = false;

                    StartBossFight();

                    return nextState;
                }
            }

            return this;
        }

        private void StartBossFight()
        {
            behaviour.Status.InvensibleMode = false;

            PlayerBehaviour.Instance.Input.GameplayInputs(true);
            AudioController.Instance.PlaySoundTrack(StageSoundTrack.BOSS_FIGHT);
        }

        private void EncounterDelayTimer()
        {
            if (encounterDelay > 0)
            {
                _currentEncounterDelay += Time.deltaTime;
                if (_currentEncounterDelay >= encounterDelay)
                {
                    behaviour.Animator.EncounterAnimation = true;

                    _currentEncounterDelay = 0;
                    _encounterDelayFinish = true;
                }
            }
            else 
            {
                behaviour.Animator.EncounterAnimation = true;

                _encounterDelayFinish = true; 
            }
        }
    }
}
