using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class BossSequencer : MonoBehaviour
    {
        #region Encapsulation
        internal State CurrentState { get => currentState; }
        #endregion

        [Header("Settings")]
        [SerializeField] private State[] bossStates;
        [SerializeField] private int[] stateExecutionSequence;

        [Space(12)]

        [SerializeField] private State currentState;
        [SerializeField] private int currentExecutionID;

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

            currentState = bossStates[0];
        }

        internal void NextSequence()
        {
            currentExecutionID++;

            if(currentExecutionID > stateExecutionSequence.Length)
            {
                currentExecutionID = 0;
                //Generate New Random Sequence
            }
        }
    }
}
