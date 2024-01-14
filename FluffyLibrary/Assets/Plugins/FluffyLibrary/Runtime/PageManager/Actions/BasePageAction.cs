using FluffyLibrary.ActionManager;

namespace FluffyLibrary.PageManager.Actions
{
    public class BasePageAction : IAction
    {
        protected bool _actionCompleted;

        public BasePageAction(BasePageManager pageManager)
        {
            PageManager = pageManager;
        }

        public BasePageManager PageManager { get; }

        public virtual bool ActionCompleted => _actionCompleted;

        public virtual void ActionInit()
        {
        }

        public virtual void ActionDeinit()
        {
        }

        public virtual void ActionStep()
        {
        }
    }
}