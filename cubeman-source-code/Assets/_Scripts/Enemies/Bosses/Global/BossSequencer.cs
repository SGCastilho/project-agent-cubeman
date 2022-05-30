using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossSequencer : MonoBehaviour
    {
        #region Encapsulation
        internal State CurrentState { get => _currentState; }
        #endregion

        [Header("Classes")]
        [SerializeField] private StateMachine stateMachine;

        [Header("Settings")]
        [SerializeField] private State[] bossStates;
        [SerializeField] private int[] stateExecutionSequence;

        [Space(12)]

        [SerializeField] private DeathState deathState;

        private State _currentState;
        private int _currentExecutionID;

        private void Awake() => SetupSequencer();

        private void SetupSequencer()
        {
            if (bossStates.Length > 0)
            {
                stateExecutionSequence = new int[bossStates.Length];

                for (int i = 0; i < stateExecutionSequence.Length; i++)
                {
                    stateExecutionSequence[i] = i;
                }
            }

            _currentState = bossStates[0];
        }

        internal void NextSequence()
        {
            _currentExecutionID++;

            if(_currentExecutionID >= stateExecutionSequence.Length)
            {
                _currentExecutionID = 0;
                //Generate New Random Sequence
            }

            _currentState = bossStates[stateExecutionSequence[_currentExecutionID]];
        }

        internal void CallDeathState()
        {
            stateMachine.SwitchToNextState(deathState);
        }
    }
}
