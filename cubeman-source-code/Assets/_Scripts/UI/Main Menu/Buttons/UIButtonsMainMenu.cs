using UnityEngine;

namespace Cubeman.UI
{
    public sealed class UIButtonsMainMenu : MonoBehaviour
    {
        public delegate void StartGameplay(string sceneToLoad);
        public event StartGameplay OnStartGameplay;

        [Header("Settings")]
        [SerializeField] private string newGameSceneToLoad = "scene-level0-prologue";

        public void ContinueButton()
        {
            Debug.Log("Continue Button");
        }

        public void NewGameButton()
        {
            OnStartGameplay?.Invoke(newGameSceneToLoad);
        }

        public void LoadGameButton()
        {
            Debug.Log("Load Game Button");
        }

        public void OptionsButton()
        {
            
        }

        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
