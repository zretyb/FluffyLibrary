using System.Collections.Generic;
using FluffyLibrary.PageManager.Controllers.Interfaces;
using FluffyLibrary.PageManager.Model;
using UnityEngine;

namespace FluffyLibrary.PageManager.Controllers
{
    public class MultiHolderLayerController : ILayerController
    {
        private readonly List<Transform> _holders = new();
        private List<Transform> _layers;

        void ILayerController.AddLayer(PageModel pageModel)
        {
            if (_layers == null) _layers = new List<Transform>();

            _layers.Add(pageModel.Page.PageObject.transform);

            var pageHolder = _holders[pageModel.LayerModel.HolderIndex];

            if (pageHolder != null) pageModel.Page.PageObject.transform.SetParent(pageHolder, false);
        }

        void ILayerController.RemoveLayer(PageModel pageModel)
        {
            if (_layers == null) return;

            if (_layers.Contains(pageModel.Page.PageObject.transform))
                _layers.Remove(pageModel.Page.PageObject.transform);
        }

        public void AddHolder(Transform holder)
        {
            _holders.Add(holder);
        }
    }
}