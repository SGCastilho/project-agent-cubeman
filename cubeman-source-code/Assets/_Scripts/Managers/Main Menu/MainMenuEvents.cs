using Cubeman.UI;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class MainMenuEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private MainMenuInputs mainMenuInputs;
        [SerializeField] private SceneLoaderManager sceneLoaderManager;

        [Space(12)]

        [SerializeField] private UIFade uiFade;
        [SerializeField] private UIMenuGroupWelcome uiMenuGroupWelcome;

        [Space(6)]

        [SerializeField] private UIButtonsMainMenu buttonsMainMenu;

        private void OnEnable() => EnableEvents();

        private void OnDestroy() => DisableEvents();

        private void Start() => UnlockCursor();

        private static void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void EnableEvents()
        {
            sceneLoaderManager.OnStartLoadScene += uiFade.LoadingFadeIn;

            buttonsMainMenu.OnStartGameplay += sceneLoaderManager.LoadScene;

            mainMenuInputs.SubscribeAnyKeyInput(uiMenuGroupWelcome.WelcomeEndTween);

            uiMenuGroupWelcome.OnWelcomeEndBegin += mainMenuInputs.UnSubscribeAnyKeyInput;
        }

        public void DisableEvents()
        {
            sceneLoaderManager.OnStartLoadScene -= uiFade.LoadingFadeIn;

            buttonsMainMenu.OnStartGameplay -= sceneLoaderManager.LoadScene;

            uiMenuGroupWelcome.OnWelcomeEndBegin -= mainMenuInputs.UnSubscribeAnyKeyInput;
        }
    }
}
