using UnityEngine;

namespace Cubeman.Enemies
{
    public class WaitState : State
    {
        #region Encapsulation
        internal float WaitDuration { set => waitDuration = value; }
        #endregion
        
        [Header("Classes")]
        [SerializeField] private BossBehaviour behaviour;

        [Header("Settings")]
        [SerializeField] [Range(1f, 12f)] private float waitDuration = 4f;

        private float _currentWaitDuration;

        public override State RunCurrentState()
        {
            _currentWaitDuration += Time.deltaTime;
            if(_currentWaitDuration >= waitDuration)
            {
                _currentWaitDuration = 0;

                return behaviour.Sequencer.CurrentState;
            }

            return this;
        }
    }
}
