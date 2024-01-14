using Cysharp.Threading.Tasks;

namespace FluffyLibrary.PageManager.Actions
{
    public class CloseAllPagesUntilAction<T> : BasePageAction where T : IPage
    {
        public CloseAllPagesUntilAction(BasePageManager pageManager)
            : base(pageManager)
        {
        }

        public override void ActionInit()
        {
            CloseAllPagesUntilAsync().Forget();
        }

        private async UniTask CloseAllPagesUntilAsync()
        {
            while (PageManager.PageController.ContainsType<T>()
                   && PageManager.PageController.TopPage().GetType() != typeof(T))
            {
                var page = PageManager.PageController.TopPage();
                await PageManager.ActionClosePageAsync(page.PageModel);
            }

            _actionCompleted = true;
        }
    }
}