using FluffyLibrary.PageManager;
using FluffyLibrary.Util;

namespace FluffyApp.Modules.Lobby
{
    public class PageLobby : BasePage
    {
        public override void TransitInStarted()
        {
            base.TransitInStarted();
            Console.Log(nameof(PageLobby), "TransitInStarted");
        }
    }
}
