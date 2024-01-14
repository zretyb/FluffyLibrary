using System.Collections.Generic;

namespace FluffyLibrary.ActionManager
{
    public class BaseActionManager<T> where T : IAction
    {
        private readonly Queue<T> _actions = new();
        private T _currentAction;

        public T GetCurrentAction()
        {
            if (_currentAction != null) return _currentAction;

            return default;
        }

        public void AddQueue(T action)
        {
            _actions.Enqueue(action);
        }

        public void ClearQueue()
        {
            _actions.Clear();
        }

        public void Step()
        {
            if (_currentAction != null)
            {
                if (!_currentAction.ActionCompleted)
                {
                    _currentAction.ActionStep();
                }
                else
                {
                    _currentAction.ActionDeinit();
                    _currentAction = default;
                }
            }
            else
            {
                if (_actions.Count > 0)
                {
                    var action = _actions.Dequeue();
                    _currentAction = action;
                    action.ActionInit();
                }
            }
        }
    }
}