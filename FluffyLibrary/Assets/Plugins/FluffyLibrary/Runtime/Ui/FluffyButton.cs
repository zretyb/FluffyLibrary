using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Console = FluffyLibrary.Util.Console;

namespace FluffyLibrary.Ui
{
    public class FluffyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected Button _button;
        
        private readonly List<Func<UniTask>> _asyncCallbacks = new();
        private readonly List<Action> _callbacks = new();
    
        private readonly List<Action> _pointerDownCallbacks = new();
        private readonly List<Action> _pointerUpCallbacks = new();
    
        public Button Button => _button;

        private void Awake()
        {
            if (_button == default)
            {
                Console.LogWarning("FluffyButton", $"{gameObject.name} Button component not found");
                return;
            }
            
            _button.onClick.AddListener(ClickAsync);
        }

        private void OnDestroy()
        {
            if (_button == default)
            {
                return;
            }
            
            _button.onClick.RemoveAllListeners();
        }

        public void OnClick(Action callback)
        {
            if (_callbacks.Contains(callback))
            {
                _callbacks.Remove(callback);
            }
            
            _callbacks.Add(callback);
        }

        public void OnClickAsync(Func<UniTask> callback)
        {
            if (_asyncCallbacks.Contains(callback))
            {
                _asyncCallbacks.Remove(callback);
            }
            
            _asyncCallbacks.Add(callback);
        }

        public void ClearOnClick()
        {
            _asyncCallbacks.Clear();
            _callbacks.Clear();
        }

        protected virtual void ClickAsync()
        {
            foreach (var callback in _callbacks.ToList())
            {
                callback.Invoke();
            }
            
            foreach (var callback in _asyncCallbacks.ToList())
            {
                callback.Invoke().Forget();
            }
        }

    public void OnPress(Action callback)
    {
        if (_pointerDownCallbacks.Contains(callback))
        {
            _pointerDownCallbacks.Remove(callback);
        }
            
        _pointerDownCallbacks.Add(callback);
    }

    public void OnRelease(Action callback)
    {
        if (_pointerUpCallbacks.Contains(callback))
        {
            _pointerUpCallbacks.Remove(callback);
        }
            
        _pointerUpCallbacks.Add(callback);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (_button == default || !_button.interactable)
        {
            return;
        }
        
        foreach (var callback in _pointerDownCallbacks.ToList())
        {
            callback.Invoke();
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (_button == default || !_button.interactable)
        {
            return;
        }
        
        foreach (var callback in _pointerUpCallbacks.ToList())
        {
            callback.Invoke();
        }
    }
    }
}
