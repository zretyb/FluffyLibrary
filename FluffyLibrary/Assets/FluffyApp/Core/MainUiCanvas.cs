using System;
using System.Collections.Generic;
using FluffyLibrary.PageManager;
using FluffyLibrary.PageManager.Controllers;
using FluffyLibrary.PageManager.Loader;
using FluffyLibrary.Util;
using UnityEngine;

namespace FluffyApp.Core
{
    public class MainUiCanvas : MonoBehaviour
    {
        private PageController _pageController;
        private AddressablePageLoader _pageLoader;
        private MultiHolderLayerController _layerController;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<Transform> _layers;
        [SerializeField] public BlockerView BlockerView;

        public void Init(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            
            var pageManager = FluffyApp.GetManager<PageManager>();
            pageManager.PageLoader = _pageLoader = new AddressablePageLoader();
            pageManager.PageController = _pageController = new PageController();
            pageManager.LayerController = _layerController = new MultiHolderLayerController();

            foreach (var layer in _layers)
            {
                _layerController.AddHolder(layer);
            }

        }
    }
}
