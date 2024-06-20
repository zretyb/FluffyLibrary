using Cysharp.Threading.Tasks;
using FluffyLibrary.ActionManager;
using FluffyLibrary.PageManager.Actions;
using FluffyLibrary.PageManager.Controllers;
using FluffyLibrary.PageManager.Controllers.Interfaces;
using FluffyLibrary.PageManager.Loader;
using FluffyLibrary.PageManager.Model;

namespace FluffyLibrary.PageManager
{
    public class BasePageManager : BaseManager
    {
        private readonly BaseActionManager<BasePageAction> _baseActionManager = new();

        public IPageLoader PageLoader { get; set; }
        public ILayerController LayerController { get; set; }
        public PageController PageController { get; set; }

        public virtual void Update()
        {
            _baseActionManager.Step();
        }

        public void OpenPage(PageModel pageModel)
        {
            var action = new OpenPageAction(this, pageModel);
            _baseActionManager.AddQueue(action);
        }

        public void ClosePage(PageModel pageModel)
        {
            var action = new ClosePageAction(this, pageModel);
            _baseActionManager.AddQueue(action);
        }

        public void CloseAllPages()
        {
            var action = new CloseAllPagesAction(this);
            _baseActionManager.AddQueue(action);
        }

        public void CloseAllPagesUntil<T>() where T : IPage
        {
            var action = new CloseAllPagesUntilAction<T>(this);
            _baseActionManager.AddQueue(action);
        }

        public void ClearPages()
        {
            _baseActionManager.ClearQueue();
            ActionClearPages();
        }


        public async UniTask ActionOpenPageAsync(PageModel pageModel)
        {
            await UniTask.DelayFrame(1);
            pageModel.PageManager = this;
            await ActionLoad(pageModel);
            await ActionAddPage(pageModel);
            await ActionAddLayer(pageModel);
            await UniTask.DelayFrame(1);
            await ActionTransitInPageAsync(pageModel);
        }

        public async UniTask ActionClosePageAsync(PageModel pageModel)
        {
            await ActionTransitOutPageAsync(pageModel);
            await ActionRemoveLayer(pageModel);
            await ActionRemovePage(pageModel);
            await ActionUnLoad(pageModel);
        }

        public void ActionClearPages()
        {
            var pages = PageController.Pages;
            foreach (var page in pages)
            {
                LayerController.RemoveLayer(page.PageModel);
                PageController.RemovePage(page);
                PageLoader.UnLoadPageAsync(page.PageModel).Forget();
            }
        }


        private async UniTask ActionLoad(PageModel pageModel)
        {
            await PageLoader.LoadPageAsync(pageModel);
        }

        private async UniTask ActionUnLoad(PageModel pageModel)
        {
            await PageLoader.UnLoadPageAsync(pageModel);
        }

        private async UniTask ActionAddPage(PageModel pageModel)
        {
            if (pageModel.Page != default)
            {
                PageController.AddPage(pageModel.Page);
                pageModel.PageAdded?.Invoke(pageModel);
                await UniTask.CompletedTask;
            }
        }

        private async UniTask ActionRemovePage(PageModel pageModel)
        {
            if (pageModel.Page != default)
            {
                pageModel.PageRemoved?.Invoke(pageModel);
                PageController.RemovePage(pageModel.Page);
                pageModel.IsPageRemoved = true;
                await UniTask.CompletedTask;
            }
        }

        private async UniTask ActionAddLayer(PageModel pageModel)
        {
            if (pageModel.Page != default)
            {
                LayerController.AddLayer(pageModel);
                await UniTask.CompletedTask;
            }
        }

        private async UniTask ActionRemoveLayer(PageModel pageModel)
        {
            if (pageModel.Page != default)
            {
                LayerController.RemoveLayer(pageModel);
                await UniTask.CompletedTask;
            }
        }

        private async UniTask ActionTransitInPageAsync(PageModel pageModel)
        {
            var page = pageModel.Page;
            if (pageModel.Page != default)
            {
                pageModel.TransitInStarted?.Invoke(pageModel);
                page.TransitInStarted();
                await page.TransitInAsync();
                page.TransitInEnded();
                pageModel.TransitInEnded?.Invoke(pageModel);
            }
        }

        private async UniTask ActionTransitOutPageAsync(PageModel pageModel)
        {
            var page = pageModel.Page;
            if (pageModel.Page != default)
            {
                pageModel.TransitOutStarted?.Invoke(pageModel);
                page.TransitOutStarted();
                await page.TransitOutAsync();
                page.TransitOutEnded();
                pageModel.TransitOutEnded?.Invoke(pageModel);
            }
        }
    }
}
