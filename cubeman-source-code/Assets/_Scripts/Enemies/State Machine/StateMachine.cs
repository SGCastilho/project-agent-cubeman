using UnityEngine;

namespace Cubeman.Enemies
{
    public sealed class StateMachine : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private State startState;
        [SerializeField] private bool startMachineOnAwake = true;

        private State currentState;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            if (!startMachineOnAwake)
            {
                enabled = false;
            }
        }

        private void OnEnable() => EnableObject();

        private void EnableObject()
        {
            currentState = startState;
        }

        void Update() => RunStateMachine();

        public void StartStateMachine()
        {
            enabled = true;
        }

        private void RunStateMachine()
        {
            State state = currentState?.RunCurrentState();

            if(state != null)
            {
                SwitchToNextState(state);
            }
        }

        internal void SwitchToNextState(State nextState)
        {
            currentState = nextState;
        }
    }
}
