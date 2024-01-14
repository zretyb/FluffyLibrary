using System.Threading;

namespace FluffyLibrary.Util
{
    public class SimpleToken
    {
        private CancellationTokenSource _tokenSource;
        public CancellationToken Token => _tokenSource?.Token ?? default;

        public CancellationToken CreateToken()
        {
            CancelToken();
            _tokenSource = new CancellationTokenSource();
            return _tokenSource.Token;
        }

        public void CancelToken()
        {
            if (_tokenSource != null) _tokenSource.Cancel();

            _tokenSource = null;
        }
    }
}