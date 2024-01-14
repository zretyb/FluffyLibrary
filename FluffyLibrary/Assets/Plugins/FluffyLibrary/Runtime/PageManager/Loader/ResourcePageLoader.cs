using Cysharp.Threading.Tasks;
using FluffyLibrary.PageManager.Model;
using FluffyLibrary.Util;
using UnityEngine;

namespace FluffyLibrary.PageManager.Loader
{
    public class ResourcePageLoader : IPageLoader
    {
        public async UniTask LoadPageAsync(PageModel pageModel)
        {
            await UniTask.CompletedTask;

            var component = default(IPage);

            var loadedPrefab = Resources.Load(pageModel.Address) as GameObject;
            if (loadedPrefab != null)
            {
                var instantiatedPrefab = Object.Instantiate(loadedPrefab);
                component = instantiatedPrefab.GetComponent(pageModel.ComponentName) as IPage;
                if (component == null)
                {
                    Object.Destroy(instantiatedPrefab);
                    Console.LogWarning("ResourcePageLoader", pageModel.ComponentName + " component does not exist! Please check Unity Project");
                }
                else
                {
                    component.SetPageModel(pageModel);
                }
            }
            else
            {
                Console.LogWarning("ResourcePageLoader", pageModel.Address + " does not exist! Please check Unity Project");
            }

            pageModel.Page = component;
        }

        public async UniTask UnLoadPageAsync(PageModel pageModel)
        {
            if (pageModel.Page != default)
            {
                Object.Destroy(pageModel.Page.PageObject);
                pageModel.Page = default;
                await UniTask.NextFrame();
            }
        }
    }
}