using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace FluffyLibrary.Util
{
    public class SimpleTimer
    {
        private readonly int _count;
        private readonly Action<SimpleTimer> _endCallback;
        private readonly int _interval;
        private readonly Action<SimpleTimer, int> _tickCallback;
        private readonly SimpleToken _token = new();

        public int Count => _count;
        
        public SimpleTimer(int interval, int count = -1, Action<SimpleTimer, int> tickCallback = null,
            Action<SimpleTimer> endCallback = null)
        {
            _interval = interval;
            _count = count;
            _tickCallback = tickCallback;
            _endCallback = endCallback;
        }

        public void Start()
        {
            _token.ResetToken();
            TimerAsync(_token.Token).Forget();
        }

        private async UniTask TimerAsync(CancellationToken token)
        {
            await UniTask.Delay(_interval, cancellationToken: token);

            var currentCount = 0;
            while (!token.IsCancellationRequested)
            {
                currentCount++;
                _tickCallback?.Invoke(this, currentCount);

                if (_count != -1 && currentCount >= _count)
                {
                    _endCallback?.Invoke(this);
                    break;
                }

                await UniTask.Delay(_interval, cancellationToken: token);
            }
        }

        public void Stop()
        {
            _token.CancelToken();
        }
    }
}
