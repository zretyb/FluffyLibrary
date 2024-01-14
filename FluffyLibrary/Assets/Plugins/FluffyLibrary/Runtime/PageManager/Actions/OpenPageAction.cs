using Cysharp.Threading.Tasks;
using FluffyLibrary.PageManager.Model;

namespace FluffyLibrary.PageManager.Actions
{
    public class OpenPageAction : BasePageAction
    {
        private readonly PageModel _pageModel;

        public OpenPageAction(BasePageManager pageManager, PageModel pageModel)
            : base(pageManager)
        {
            _pageModel = pageModel;
        }

        public override void ActionInit()
        {
            OpenPageAsync().Forget();
        }

        private async UniTask OpenPageAsync()
        {
            await PageManager.ActionOpenPageAsync(_pageModel);
            _actionCompleted = true;
        }
    }
}