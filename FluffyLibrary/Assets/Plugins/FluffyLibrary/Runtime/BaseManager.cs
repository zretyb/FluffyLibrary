using UnityEngine;

namespace FluffyLibrary
{
    public class BaseManager : MonoBehaviour
    {
        public static T Create<T>(string managerName, Transform root) where T : BaseManager
        {
            managerName = string.IsNullOrEmpty(managerName) ? typeof(T).Name : managerName;
            var manager = new GameObject(managerName, typeof(T))
            {
                transform = { parent = root }
            };

            var result = manager.GetComponent<T>();
            return result;
        }

        public static T Create<T>(Transform root) where T : BaseManager
        {
            var manager = new GameObject(typeof(T).Name, typeof(T))
            {
                transform = { parent = root }
            };

            var result = manager.GetComponent<T>();
            return result;
        }

        public virtual void SlowUpdate()
        {
        }

        public virtual void Clear()
        {
        }
    }
}
