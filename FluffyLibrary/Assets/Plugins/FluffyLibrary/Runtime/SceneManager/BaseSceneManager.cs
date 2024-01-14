using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FluffyLibrary.SceneManager
{
    public class BaseSceneManager : MonoBehaviour
    {
        private string _currentSceneName;
        private bool _isLoadingScene;

        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        ///     Load scene with loading screen
        /// </summary>
        /// <param name="sceneName">Name of the scene to be loaded</param>
        /// <param name="forceReload">Force reload a scene even if it is currently active</param>
        /// <returns></returns>
        public async UniTask<bool> LoadSceneAsync(string sceneName, bool forceReload = false)
        {
            if (_isLoadingScene)
                // A scene is loading in progress
                return false;

            if (!forceReload && _currentSceneName == sceneName)
                // Scene is already loaded
                return false;

            // set state variables
            _isLoadingScene = true;
            var activeScreenName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            // show loading scene
            await ShowLoadingSceneAsync();

            // unload current active screen
            await UnloadSceneAsync(string.IsNullOrEmpty(_currentSceneName) ? activeScreenName : _currentSceneName);

            // load new scene
            var newScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            await newScene;

            // hide loading scene
            await HideLoadingSceneAsync();

            // set state variables
            _currentSceneName = sceneName;
            _isLoadingScene = false;

            return true;
        }

        private async UniTask UnloadSceneAsync(string sceneName)
        {
            await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
        }

        // TODO Remove Hardcoded loading scene name
        private async UniTask ShowLoadingSceneAsync()
        {
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        }

        // TODO Remove Hardcoded loading scene name
        private async UniTask HideLoadingSceneAsync()
        {
            await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("LoadingScene");
        }
    }
}