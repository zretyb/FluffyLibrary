using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Console = FluffyLibrary.Util.Console;

namespace FluffyLibrary.Ui
{
    public partial class FluffyButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        
        protected readonly List<Func<UniTask>> _asyncCallbacks = new();
        protected readonly List<Action> _callbacks = new();

        public virtual void SetLabel(string value)
        {
        }

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

        private void ClickAsync()
        {
            ClickAction();
            foreach (var callback in _callbacks)
            {
                callback.Invoke();
            }
            
            foreach (var callback in _asyncCallbacks)
            {
                callback.Invoke().Forget();
            }
        }

        protected virtual void ClickAction()
        {
        }
    }
}
