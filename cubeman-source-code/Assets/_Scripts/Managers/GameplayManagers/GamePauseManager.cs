using System.Collections;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class GamePauseManager : MonoBehaviour
    {
        #region Singleton
        public static GamePauseManager Instance;
        #endregion
        
        #region Encapsulation
        public bool BlockPause { set => _blockPause = value; }
        #endregion

        public delegate void PauseGame();
        public event PauseGame OnPauseGame;

        public delegate void UnPauseGame();
        public event UnPauseGame OnUnPauseGame;

        [Header("Classes")]
        [SerializeField] private GameTimeManager timeManager;

        [Header("Settings")]
        [SerializeField] [Range(0.6f, 2f)] private float couldownBetweenPauses = 2f;

        private bool _blockPause;
        private bool _gamePaused;
        private bool _readyToPause;
        private bool _readyToUnPause;

        private void Awake() => Instance = this;

        private void OnEnable() => SetupObject();

        private void SetupObject()
        {
            _readyToPause = true;
            _blockPause = true;
        }

        public void Pause()
        {
            if(_blockPause || _gamePaused || !_readyToPause) return;

            OnPauseGame?.Invoke();
            timeManager.StopTime();

            StartCoroutine(UnPauseCouldownCoroutine());

            _readyToPause = false;
            _gamePaused = true;
        }

        IEnumerator UnPauseCouldownCoroutine()
        {
            yield return new WaitForSecondsRealtime(couldownBetweenPauses);
            _readyToUnPause = true;
        }

        public void UnPause()
        {
            if(_blockPause || !_gamePaused || !_readyToUnPause) return;

            OnUnPauseGame?.Invoke();
            timeManager.ResumeTime();
            
            StartCoroutine(PauseCouldownCoroutine());

            _readyToUnPause = false;
            _gamePaused = false;
        }

        IEnumerator PauseCouldownCoroutine()
        {
            yield return new WaitForSecondsRealtime(couldownBetweenPauses);
            _readyToPause = true;
        }

        public void BlockPauseEvent()
        {
            _blockPause = true;
        }
    }
}