using FluffyLibrary.PageManager;
using FluffyLibrary.PageManager.Model;

namespace FluffyApp.Core
{
    public class PageManager : BasePageManager
    {
        private static string PageAddress = "Page/{0}";
        public enum PageLayer
        {
            Base = 0,
            Default = 1,
            Dialogue = 2,
            Overlay = 3,
        }

        public PageModelT<TData> OpenPage<T, TData>(TData data, PageLayer pageLayer) where T : IPage
        {
            var componentName = typeof(T).Name;
            var layerModel = new LayerModel();
            layerModel.SetHolderIndex((int)pageLayer);
            
            var pageModel = new PageModelT<TData>()
            {
                ComponentName = componentName,
                LayerModel = layerModel,
                Address = string.Format(PageAddress, componentName),
                Data = data
            };
            
            OpenPage(pageModel);

            return pageModel;
        }
    }
}
