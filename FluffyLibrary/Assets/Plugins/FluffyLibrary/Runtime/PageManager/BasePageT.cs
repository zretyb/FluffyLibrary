using FluffyLibrary.PageManager.Model;

namespace FluffyLibrary.PageManager
{
    public class BasePageT<T> : BasePage
    {
        public T Data { get; private set; }

        public override void SetPageModel(PageModel pageModel)
        {
            base.SetPageModel(pageModel);
            if (pageModel is PageModelT<T> pageModelT)
            {
                Data = pageModelT.Data;
            }
        }
    }
}
