using System.Collections.Generic;
using FluffyLibrary.PageManager.Controllers;
using FluffyLibrary.PageManager.Loader;
using FluffyLibrary.Util;
using UnityEngine;

namespace FluffyApp.Core
{
    public class MainUiCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<Transform> _layers;
        [SerializeField] public BlockerView BlockerView;

        public Canvas Canvas => _canvas;

        public void Init(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            
            var pageManager = FluffyApp.GetManager<PageManager>();
            pageManager.PageLoader = new AddressablePageLoader();
            pageManager.PageController = new PageController();
            var layerController = new MultiHolderLayerController();
            pageManager.LayerController = layerController;

            foreach (var layer in _layers)
            {
                layerController.AddHolder(layer);
            }
        }
    }
}
