using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FluffyLibrary.Util
{
    public class SimplePrefabPool<T> where T : Component
    {
        private Stack<T> _list = new ();
        private T _component;
        private Transform _poolHolder;
        private List<T> _spawnedList = new ();

        public int SpawnedCount => _spawnedList.Count;

        public List<T> SpawnedList => _spawnedList.ToList();
        
        public SimplePrefabPool(T component)
        {
            _component = component;
            _poolHolder = component.transform.parent;
        }
        
        public SimplePrefabPool(T component, Transform poolHolder)
        {
            _component = component;
            _poolHolder = poolHolder;
        }

        public T Spawn()
        {
            return Spawn(_poolHolder);
        }

        public T Spawn(Transform target)
        {
            if (_list.Count > 0)
            {
                var fromPool = _list.Pop();
                if (target == _poolHolder)
                {
                    fromPool.transform.SetAsLastSibling();
                }
                else
                {
                    fromPool.transform.SetParent(target);
                }
                if (!_spawnedList.Contains(fromPool))
                {
                    _spawnedList.Add(fromPool);
                }
                return fromPool;
            }

            var result = Object.Instantiate(_component, target);
            if (!_spawnedList.Contains(result))
            {
                _spawnedList.Add(result);
            }
            return result;
        }

        public void Return(T component)
        {
            component.transform.SetParent(_poolHolder);
            if (_spawnedList.Contains(component))
            {
                _spawnedList.Remove(component);
            }
            _list.Push(component);
        }

        public List<T> ReturnAll()
        {
            var returned = new List<T>();
            foreach (var component in _spawnedList.Where(component => component != default))
            {
                component.transform.SetParent(_poolHolder);
                _list.Push(component);
                returned.Add(component);
            }
            
            _spawnedList.Clear();

            return returned;
        }
    }
}
