using UnityEngine;

namespace FluffyLibrary.Extensions
{
    public static class ComponentExtensions
    {
        public static T FGetComp<T>(this GameObject gameObject) where T : Component
        {
            var comp = gameObject.GetComponent<T>();

            if (comp == default) comp = gameObject.AddComponent<T>();

            return comp;
        }
    }
}