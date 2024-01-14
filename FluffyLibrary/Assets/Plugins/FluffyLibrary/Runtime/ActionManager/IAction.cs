namespace FluffyLibrary.ActionManager
{
    public interface IAction
    {
        bool ActionCompleted { get; }
        void ActionInit();
        void ActionStep();
        void ActionDeinit();
    }
}