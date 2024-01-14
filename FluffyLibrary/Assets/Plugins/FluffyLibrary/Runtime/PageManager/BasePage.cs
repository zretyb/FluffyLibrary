using Cysharp.Threading.Tasks;
using FluffyLibrary.PageManager.Model;
using UnityEngine;

namespace FluffyLibrary.PageManager
{
    public class BasePage : MonoBehaviour, IPage
    {
        public GameObject PageObject => gameObject;

        public PageModel PageModel { get; private set; }

        public virtual void SetPageModel(PageModel pageModel)
        {
            PageModel = pageModel;
        }

        public virtual T GetPage<T>() where T : Component, IPage
        {
            return this as T;
        }

        public virtual void TransitInStarted()
        {
        }

        public virtual void TransitOutStarted()
        {
        }

        public virtual void TransitInEnded()
        {
        }

        public virtual void TransitOutEnded()
        {
        }

        public virtual async UniTask TransitInAsync()
        {
            await UniTask.CompletedTask;
        }

        public virtual async UniTask TransitOutAsync()
        {
            await UniTask.CompletedTask;
        }
    }
}