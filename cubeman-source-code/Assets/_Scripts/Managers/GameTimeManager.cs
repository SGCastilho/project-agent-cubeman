using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class GameTimeManager : MonoBehaviour
    {
        #region Encapsulation
        public bool TimeStoped { get => _timeStoped; }
        #endregion

        private bool _timeStoped;

        private void Awake() => Time.timeScale = 1;

        public void ResumeTime()
        {
            if(!_timeStoped) return;

            Time.timeScale = 1;
            _timeStoped = false;
        }

        public void StopTime()
        {
            if(_timeStoped) return;

            Time.timeScale = 0;
            _timeStoped = true;
        }
    }
}