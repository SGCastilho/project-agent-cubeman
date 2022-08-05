using UnityEngine;

namespace Cubeman.UI
{
    public sealed class PauseMenuButtons : MonoBehaviour
    {
        private const string MENU_SCENE_NAME = "scene-menu-mainMenu";

        public delegate void Resume();
        public event Resume OnResume;

        public delegate void StartQuit();
        public event StartQuit OnStartQuit;

        public delegate void Quit(string sceneToLoad);
        public event Quit OnQuit;

        [Header("Classes")]
        [SerializeField] private CanvasGroup canvasGroup;

        public void ResumeButton() => OnResume.Invoke();

        public void OptionsButton()
        {
            Debug.Log("Options Menu :)");
        }

        public void QuitButton() 
        {
            canvasGroup.blocksRaycasts = false;

            OnStartQuit?.Invoke();
            OnQuit?.Invoke(MENU_SCENE_NAME);
        }
    }
}