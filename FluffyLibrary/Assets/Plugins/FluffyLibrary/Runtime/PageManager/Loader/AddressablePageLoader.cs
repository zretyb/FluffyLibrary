using Cysharp.Threading.Tasks;
using FluffyLibrary.PageManager.Model;
using FluffyLibrary.Util;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FluffyLibrary.PageManager.Loader
{
    public class AddressablePageLoader : IPageLoader
    {
        public async UniTask LoadPageAsync(PageModel pageModel)
        {
            await UniTask.CompletedTask;

            var component = default(IPage);

            var instantiatedPrefab = await Addressables.InstantiateAsync(pageModel.Address);
            if (instantiatedPrefab != null)
            {
                component = instantiatedPrefab.GetComponent(pageModel.ComponentName) as IPage;
                if (component == null)
                {
                    Addressables.ReleaseInstance(instantiatedPrefab);
                    Console.LogWarning("AddressablePageLoader", pageModel.ComponentName + " component does not exist! Please check Unity Project");
                }
                else
                {
                    component.SetPageModel(pageModel);
                }
            }
            else
            {
                Console.LogWarning("AddressablePageLoader", pageModel.Address + " does not exist! Please check Unity Project");
            }

            pageModel.Page = component;
        }

        public async UniTask UnLoadPageAsync(PageModel pageModel)
        {
            if (pageModel.Page != default)
            {
                Addressables.ReleaseInstance(pageModel.Page.PageObject);
                pageModel.Page = default;
                await UniTask.NextFrame();
            }
        }
    }
}
