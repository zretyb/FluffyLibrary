using System.Collections.Generic;
using FluffyLibrary.PageManager.Controllers.Interfaces;
using FluffyLibrary.PageManager.Model;
using UnityEngine;

namespace FluffyLibrary.PageManager.Controllers
{
    public class LayerController : ILayerController
    {
        private List<Transform> layers;

        private Transform pageHolder;

        public LayerController(Transform pageHolder)
        {
            this.pageHolder = pageHolder;
            layers = new List<Transform>();
        }

        public Transform PageHolder
        {
            set => pageHolder = value;
        }

        public void AddLayer(PageModel model)
        {
            if (layers == null)
                layers = new List<Transform>();

            layers.Add(model.Page.PageObject.transform);

            if (pageHolder != null)
                model.Page.PageObject.transform.SetParent(pageHolder, false);

            SortLayers();
        }

        public void RemoveLayer(PageModel model)
        {
            if (model.Page.PageObject == null)
                return;

            if (layers.Contains(model.Page.PageObject.transform)) layers.Remove(model.Page.PageObject.transform);

            SortLayers();
        }

        private void SortLayers()
        {
            var count = 0;
            foreach (var layer in layers)
            {
                layer.SetSiblingIndex(count);
                count++;
            }
        }
    }
}