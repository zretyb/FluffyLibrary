using UnityEngine;

namespace FluffyLibrary.Util
{
    public class BlockerView : MonoBehaviour
    {
        [SerializeField] private GameObject blocker;

        private int _counter;

        private bool _destroyed;

        private void Awake()
        {
            blocker.SetActive(false);
        }

        private void OnDestroy()
        {
            _destroyed = true;
        }

        public void ShowBlocker()
        {
            if (_destroyed) return;

            _counter++;
            blocker.SetActive(true);
        }

        public void HideBlocker()
        {
            if (_destroyed) return;

            _counter--;
            if (_counter <= 0)
            {
                blocker.SetActive(false);
                _counter = 0;
            }
        }
    }
}