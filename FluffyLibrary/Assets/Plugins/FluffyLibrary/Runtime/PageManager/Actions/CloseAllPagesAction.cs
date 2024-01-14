using Cysharp.Threading.Tasks;

namespace FluffyLibrary.PageManager.Actions
{
    public class CloseAllPagesAction : BasePageAction
    {
        public CloseAllPagesAction(BasePageManager pageManager)
            : base(pageManager)
        {
        }

        public override void ActionInit()
        {
            CloseAllPagesAsync().Forget();
        }

        private async UniTask CloseAllPagesAsync()
        {
            while (PageManager.PageController.HasPage())
            {
                var page = PageManager.PageController.TopPage();
                var pageModel = page.PageModel;
                await PageManager.ActionClosePageAsync(pageModel);
            }

            _actionCompleted = true;
        }
    }
}