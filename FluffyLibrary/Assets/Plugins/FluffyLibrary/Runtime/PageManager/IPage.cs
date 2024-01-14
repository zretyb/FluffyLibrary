using FluffyLibrary.PageManager.Model;
using FluffyLibrary.Transitable;
using UnityEngine;

namespace FluffyLibrary.PageManager
{
    public interface IPage : ITransitable
    {
        GameObject PageObject { get; }
        PageModel PageModel { get; }

        void SetPageModel(PageModel pageModel);

        T GetPage<T>() where T : Component, IPage;
    }
}