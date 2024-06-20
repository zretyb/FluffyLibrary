using System;

namespace FluffyLibrary.PageManager.Model
{
    public class PageModel<T> : PageModel
    {
        public T Data { get; set; }
    }
    
    public class PageModel
    {
        private LayerModel _layerModel;

        public Action<PageModel> PageAdded;
        public Action<PageModel> PageRemoved;
        
        public Action<PageModel> TransitInStarted;
        public Action<PageModel> TransitInEnded;
        
        public Action<PageModel> TransitOutStarted;
        public Action<PageModel> TransitOutEnded;

        public bool IsPageRemoved;


        public string ComponentName { get; set; }
        public string Address { get; set; }
        public IPage Page { get; set; }

        public BasePageManager PageManager { get; set; }

        public LayerModel LayerModel
        {
            get => _layerModel ?? new LayerModel().SetHolderIndex(0);
            set => _layerModel = value;
        }
    }
}
