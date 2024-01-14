using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FluffyApp.Core
{
    public class SetupScene : MonoBehaviour
    {
        [SerializeField] private MainUiCanvas _mainUiCanvas;
        
        void Start()
        {
            InitAsync().Forget();
        }

        private async UniTask InitAsync()
        {
            if (FluffyApp.Instance.MainUiCanvas == default)
            {
                DontDestroyOnLoad(_mainUiCanvas.gameObject);
                _mainUiCanvas.Init(FluffyApp.GetManager<CameraManager>().UiCamera);
                FluffyApp.Instance.MainUiCanvas = _mainUiCanvas;
            }

            SceneManager.LoadScene("LobbyScene");

            await UniTask.CompletedTask;
        }
    }
}
