using Cysharp.Threading.Tasks;
using FluffyLibrary.PageManager.Model;

namespace FluffyLibrary.PageManager.Loader
{
    public interface IPageLoader
    {
        UniTask LoadPageAsync(PageModel pageModel);
        UniTask UnLoadPageAsync(PageModel pageModel);
    }
}