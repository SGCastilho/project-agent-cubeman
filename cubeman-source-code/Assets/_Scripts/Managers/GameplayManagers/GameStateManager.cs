using Cubeman.Player;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class GameStateManager : MonoBehaviour
    {
        private enum GameState { START, GAMEPLAY, WIN, LOSE }

        public delegate void Win();
        public event Win OnWin;

        public delegate void Lose();
        public event Lose OnLose;

        [Header("Settings")]
        [SerializeField] private GameState currentState;

        private PlayerBehaviour _player;

        private void Awake() => _player = FindObjectOfType<PlayerBehaviour>();

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
            OnWin?.Invoke();
        }

        private void LoseState()
        {
            OnLose?.Invoke();
        }
    }
}
