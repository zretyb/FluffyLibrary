using FluffyLibrary.PageManager.Model;

namespace FluffyLibrary.PageManager.Controllers.Interfaces
{
    public interface ILayerController
    {
        void AddLayer(PageModel model);
        void RemoveLayer(PageModel model);
    }
}