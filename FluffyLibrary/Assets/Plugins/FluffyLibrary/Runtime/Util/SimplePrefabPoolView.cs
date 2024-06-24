using System;
using System.Collections.Generic;
using System.Linq;
using FluffyLibrary.Util;
using UnityEngine;

namespace FluffyLibrary
{
    public class SimplePrefabPoolView<TModel, TComp> : MonoBehaviour where TComp : Component
    {
        public Action<TModel, TComp> CustomUpdateCompAction;
        
        [SerializeField] protected TComp _comp; 
        
        private SimplePrefabPool<TComp> _compPool;
        protected readonly List<TModel> _models = new();
        protected readonly Dictionary<TModel, TComp> _modelToCompMapping = new();

        private bool _inited;

        public List<TComp> SpawnedList => _compPool.SpawnedList;

        public void Awake()
        {
            Init();
            _comp.gameObject.SetActive(false);
        }

        private void Init()
        {
            if (_inited)
            {
                return;
            }
            
            _compPool = new SimplePrefabPool<TComp>(_comp, transform);
            _inited = true;
        }

        public void SetModels(List<TModel> models)
        {
            ClearAll();
            AddModels(models);
        }

        public TComp GetComp(TModel model)
        {
            _modelToCompMapping.TryGetValue(model, out var result);
            return result;
        }

        public virtual TComp AddModel(TModel model)
        {
            _models.Add(model);
            var comp = _compPool.Spawn(transform);
            UpdateComp(model, comp);
            _modelToCompMapping[model] = comp;
            return comp;
        }
        
        public virtual void AddModels(List<TModel> models)
        {
            Init();
            _models.AddRange(models);
            for (var i = 0; i < models.Count; i++)
            {
                var model = models[i];
                var comp = _compPool.Spawn(transform);
                UpdateComp(model, comp);
                _modelToCompMapping[model] = comp;
            }
        }

        protected virtual void UpdateComp(TModel model, TComp comp)
        {
            comp.gameObject.SetActive(true);
            CustomUpdateCompAction?.Invoke(model, comp);
        }

        public void ClearAll()
        {
            
            if (!_inited)
            {
                return;
            }
            
            foreach (var comp in _compPool.ReturnAll())
            {
                comp.gameObject.SetActive(false);
            }
            
            _models.Clear();
            _modelToCompMapping.Clear();
        }
    }
}
