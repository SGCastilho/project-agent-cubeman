using Cubeman.UI;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class MainMenuEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private SceneLoaderManager sceneLoaderManager;

        [Space(12)]

        [SerializeField] private UIFade uiFade;

        [Space(6)]

        [SerializeField] private UIButtonsMainMenu buttonsMainMenu;

        private void OnEnable() => EnableEvents();

        private void OnDestroy() => DisableEvents();

        public void EnableEvents()
        {
            sceneLoaderManager.OnStartLoadScene += uiFade.FadeIn;

            buttonsMainMenu.OnStartGameplay += sceneLoaderManager.LoadScene;
        }

        public void DisableEvents()
        {
            sceneLoaderManager.OnStartLoadScene -= uiFade.FadeIn;

            buttonsMainMenu.OnStartGameplay -= sceneLoaderManager.LoadScene;
        }
    }
}
