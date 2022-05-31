using Cubeman.Audio;
using Cubeman.Player;
using System.Collections;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class GameStateManager : MonoBehaviour
    {
        private enum GameState { START, GAMEPLAY, WIN, LOSE }

        public delegate void CompleteStageMessage();
        public event CompleteStageMessage OnCompleteStageMessage; 

        public delegate void Win(string sceneToLoad);
        public event Win OnWin;

        public delegate void Lose();
        public event Lose OnLose;

        private PlayerBehaviour _player;

        [Header("Settings")]
        [SerializeField] private GameState currentState;

        [Space(12)]

        [SerializeField] private string nextSceneToLoad;

        [Space(12)]

        [SerializeField] private AudioClip completeAudioClip;
        [SerializeField] [Range(0.1f, 1f)] private float completeAudioClipVolumeScale = 0.8f;

        private float _winStateDuration;

        private void Awake() => SetupObject();

        private void SetupObject()
        {
            _player = FindObjectOfType<PlayerBehaviour>();

            _winStateDuration = completeAudioClip.length;
        }

        private void ChangeGameState(GameState nextState)
        {
            if(nextState != currentState)
            {
                currentState = nextState;
                switch (currentState)
                {
                    case GameState.GAMEPLAY:
                        GameplayState();
                        break;
                    case GameState.WIN:
                        WinState();
                        break;
                    case GameState.LOSE:
                        LoseState();
                        break;
                }
            }
        }

        public void GameStart() => ChangeGameState(GameState.GAMEPLAY);

        public void GameWin() => ChangeGameState(GameState.WIN);

        public void GameLose() => ChangeGameState(GameState.LOSE);

        private void GameplayState() => _player.Input.GameplayInputs(true);

        private void WinState()
        {
            AudioController.Instance.PlaySoundEffect(ref completeAudioClip, completeAudioClipVolumeScale);

            StartCoroutine(WinStateCoroutine());
        }

        IEnumerator WinStateCoroutine()
        {
            yield return new WaitForSeconds(_winStateDuration);
            OnCompleteStageMessage?.Invoke();
            yield return new WaitForSeconds(2.8f);
            OnWin?.Invoke(nextSceneToLoad);
        }

        private void LoseState()
        {
            OnLose?.Invoke();
        }
    }
}
