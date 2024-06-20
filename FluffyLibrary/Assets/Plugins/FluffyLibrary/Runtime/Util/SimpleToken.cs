using System;
using System.Threading;

namespace FluffyLibrary.Util
{
    public class SimpleToken : IDisposable
    {
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public CancellationToken Token => _tokenSource.Token;

        public CancellationToken ResetToken()
        {
            CancelToken();
            _tokenSource = new CancellationTokenSource();
            return _tokenSource.Token;
        }

        public void CancelToken()
        {
            _tokenSource?.Cancel();
            _tokenSource = null;
        }

        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
