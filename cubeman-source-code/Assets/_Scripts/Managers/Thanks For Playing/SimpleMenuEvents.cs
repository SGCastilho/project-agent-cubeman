using Cubeman.UI;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class SimpleMenuEvents : MonoBehaviour
    {
        [Header("Classes")]
        [SerializeField] private SceneLoaderManager sceneLoader;

        [Space(12)]

        [SerializeField] private UIFade uiFade;

        private void OnEnable() 
        {
            sceneLoader.OnStartLoadScene += uiFade.LoadingFadeIn;
        }

        private void OnDisable() 
        {
            sceneLoader.OnStartLoadScene -= uiFade.LoadingFadeIn;
        }
    }
}