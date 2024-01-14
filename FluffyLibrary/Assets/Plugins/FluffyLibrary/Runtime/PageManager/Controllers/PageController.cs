using System;
using System.Collections.Generic;
using System.Linq;

namespace FluffyLibrary.PageManager.Controllers
{
    public class PageController
    {
        private List<IPage> pages;

        public List<IPage> Pages => pages.ToList();
        public event Action<IPage> EvtPageAdded;
        public event Action<IPage> EvtPageRemoved;

        public void AddPage(IPage page)
        {
            if (pages == null)
                pages = new List<IPage>();

            pages.Add(page);
            EvtPageAdded?.Invoke(page);
        }

        public void RemovePage(IPage page)
        {
            if (!pages.Contains(page))
                return;

            pages.Remove(page);
            EvtPageRemoved?.Invoke(page);
        }

        public bool HasPage()
        {
            return pages != null && pages.Count > 0;
        }

        public bool ContainsPage(IPage page)
        {
            return pages != null && pages.Contains(page);
        }

        public bool ContainsType(Type type)
        {
            if (pages != null)
                foreach (var page in pages)
                    if (page.GetType() == type)
                        return true;

            return false;
        }

        public bool ContainsType<T>() where T : IPage
        {
            if (pages != null)
                foreach (var page in pages)
                    if (page.GetType() == typeof(T))
                        return true;

            return false;
        }

        public T GetPageByType<T>() where T : IPage
        {
            if (pages != null)
                foreach (var page in pages)
                    if (page.GetType() == typeof(T))
                        return (T)page;
            return default;
        }

        public List<T> GetPagesByType<T>() where T : IPage
        {
            var result = new List<T>();
            if (pages != null)
                foreach (var page in pages)
                    if (page.GetType() == typeof(T))
                        result.Add((T)page);
            return result;
        }

        public IPage GetPageByType(Type type)
        {
            if (pages != null)
                foreach (var page in pages)
                    if (page.GetType() == type)
                        return page;

            return default;
        }

        public IPage GetPageByIndex(int index)
        {
            if (pages != null && pages.Count > index) return pages[index];

            return default;
        }

        //public T GetPageBySubType<T>() where T : IPage
        //{
        //    if (pages != null)
        //        foreach (IPage page in pages)
        //            if (page.GetType().IsSubclassOf(typeof(T)))
        //                return (T)page;
        //    return default(T);
        //}

        public int NumPages()
        {
            if (pages == null)
                return 0;

            return pages.Count;
        }

        public IPage PageBelowTopPage()
        {
            var numPages = NumPages();
            if (numPages < 2) return null;

            return pages[numPages - 2];
        }

        public IPage TopPage()
        {
            if (!HasPage())
                return null;

            return pages.Last();
        }

        public bool IsTopPage(IPage page)
        {
            if (!HasPage())
                return false;

            return pages.Last() == page;
        }
    }
}