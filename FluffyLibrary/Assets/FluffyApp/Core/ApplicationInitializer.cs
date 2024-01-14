using Cysharp.Threading.Tasks;
using FluffyLibrary.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FluffyApp.Core
{
    public class ApplicationInitializer : MonoBehaviour
    {
        private void Start()
        {
            InitAsync().Forget();            
        }

        private async UniTask InitAsync()
        {
            Console.Log("ApplicationInitializer", "Init Async");
            
            FluffyApp.GetManager<CameraManager>().Init();

            SceneManager.LoadScene("SetupScene");

            await UniTask.CompletedTask;
        }
    }
}
