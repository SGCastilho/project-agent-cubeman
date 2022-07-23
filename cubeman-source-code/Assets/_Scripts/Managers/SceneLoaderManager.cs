using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Cubeman.Manager
{
    public sealed class SceneLoaderManager : MonoBehaviour
    {
        internal delegate void StartLoadScene();
        internal event StartLoadScene OnStartLoadScene;

        public void LoadActiveScene()
        {
            if(OnStartLoadScene != null) { OnStartLoadScene(); }

            var currentScene = SceneManager.GetActiveScene();
            StartCoroutine(LoadingSceneCoroutine(currentScene.buildIndex));
        }

        public void LoadScene(int sceneIndex)
        {
            if (OnStartLoadScene != null) { OnStartLoadScene(); }

            StartCoroutine(LoadingSceneCoroutine(sceneIndex));
        }

        public void LoadScene(string sceneName)
        {
            if (OnStartLoadScene != null) { OnStartLoadScene(); }

            StartCoroutine(LoadingSceneCoroutine(sceneName));
        }

        public void LoadScene(Scene scene)
        {
            if (OnStartLoadScene != null) { OnStartLoadScene(); }

            StartCoroutine(LoadingSceneCoroutine(scene));
        }

        IEnumerator LoadingSceneCoroutine(int sceneIndex)
        {
            yield return new WaitForSecondsRealtime(0.6f);

            var sceneLoading = SceneManager.LoadSceneAsync(sceneIndex);

            sceneLoading.completed += CheckTimeScale;

            while (!sceneLoading.isDone)
            {
                var progress = Mathf.Clamp01(sceneLoading.progress / 0.9f);
                yield return null;
            }
        }

        IEnumerator LoadingSceneCoroutine(string sceneName)
        {
            yield return new WaitForSecondsRealtime(0.6f);

            var sceneLoading = SceneManager.LoadSceneAsync(sceneName);

            sceneLoading.completed += CheckTimeScale;

            while (!sceneLoading.isDone)
            {
                var progress = Mathf.Clamp01(sceneLoading.progress / 0.9f);
                yield return null;
            }
        }

        IEnumerator LoadingSceneCoroutine(Scene scene)
        {
            var sceneToLoad = scene.buildIndex;

            yield return new WaitForSecondsRealtime(0.6f);

            var sceneLoading = SceneManager.LoadSceneAsync(sceneToLoad);

            sceneLoading.completed += CheckTimeScale;

            while (!sceneLoading.isDone)
            {
                var progress = Mathf.Clamp01(sceneLoading.progress / 0.9f);
                yield return null;
            }
        }

        private void CheckTimeScale(AsyncOperation obj)
        {
            if(Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }
        }
    }
}
