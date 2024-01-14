using FluffyApp.Core;
using UnityEngine;

namespace FluffyApp.Modules.Lobby
{
    public class LobbyScene : MonoBehaviour
    {
        public void Start()
        {
            FluffyApp.GetManager<PageManager>().OpenPage<PageLobby, string>("Lobby", PageManager.PageLayer.Default);
        }
    }
}
