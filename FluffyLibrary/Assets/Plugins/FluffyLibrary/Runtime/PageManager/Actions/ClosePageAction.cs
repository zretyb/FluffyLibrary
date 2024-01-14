using Cysharp.Threading.Tasks;
using FluffyLibrary.PageManager.Model;

namespace FluffyLibrary.PageManager.Actions
{
    public class ClosePageAction : BasePageAction
    {
        private readonly PageModel _pageModel;

        public ClosePageAction(BasePageManager pageManager, PageModel pageModel)
            : base(pageManager)
        {
            _pageModel = pageModel;
        }

        public override void ActionInit()
        {
            ClosePageAsync().Forget();
        }

        private async UniTask ClosePageAsync()
        {
            while (_pageModel.Page != default && PageManager.PageController.ContainsPage(_pageModel.Page))
            {
                var page = PageManager.PageController.TopPage();
                var pageModel = page.PageModel;
                await PageManager.ActionClosePageAsync(pageModel);
            }

            _actionCompleted = true;
        }
    }
}