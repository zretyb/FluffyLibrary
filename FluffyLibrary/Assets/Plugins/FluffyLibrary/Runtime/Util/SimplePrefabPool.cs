using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FluffyLibrary.Util
{
    public class SimplePrefabPool<T> where T : Component
    {
        private readonly T _component;
        private readonly Stack<T> _list = new();
        private readonly List<T> _spawned = new();
        private readonly Transform _poolHolder;

        public SimplePrefabPool(T component, Transform poolHolder)
        {
            _component = component;
            _poolHolder = poolHolder;
            _component.transform.SetParent(_poolHolder, false);
        }

        public T Spawn(Transform target)
        {
            if (_list.Count > 0)
            {
                var fromPool = _list.Pop();
                fromPool.transform.SetParent(target);
                _spawned.Add(fromPool);
                return fromPool;
            }

            var result = Object.Instantiate(_component, target);
            _spawned.Add(result);
            return result;
        }

        public void Return(T component)
        {
            if (_spawned.Contains(component))
            {
                _spawned.Remove(component);
            }

            component.transform.SetParent(_poolHolder);
            _list.Push(component);
        }

        public void ReturnAll()
        {
            foreach (var spawned in _spawned.ToList())
            {
                Return(spawned);
            }
        }
    }
}