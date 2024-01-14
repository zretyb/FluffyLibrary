using System;
using System.Collections.Generic;
using FluffyApp.Core;
using FluffyLibrary;
using UnityEngine;

namespace FluffyApp
{
    public class FluffyApp : Singleton<FluffyApp>
    {
        private Transform _managerHolder;

        private readonly Dictionary<Type, BaseManager> _managers = new();

        public MainUiCanvas MainUiCanvas { get; set; }

        public void Awake()
        {
            _managerHolder = new GameObject("Managers").transform;
            _managerHolder.parent = transform;
        }

        public static T GetManager<T>() where T : BaseManager
        {
            if (Instance._managers.TryGetValue(typeof(T), out var result))
            {
                return (T)result;
            }

            result = BaseManager.Create<T>(Instance._managerHolder);
            Instance._managers[typeof(T)] = result;

            return  (T)result;
        }
    }
}
