using System.Threading;
using Microsoft.Extensions.Primitives;

namespace Application.Cache
{
    public static class AssetTokenSourceProvider
    {
        private static CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private static CancellationChangeToken _changeToken;

        public static IChangeToken GetCancellationToken()
        {
            return _changeToken ??= new CancellationChangeToken(_tokenSource.Token);
        }

        public static void ResetCancellationToken()
        {
            if (_tokenSource is {IsCancellationRequested: false, Token: {CanBeCanceled: true}})
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }

            _tokenSource = new CancellationTokenSource();
            _changeToken = null;
        }
    }
}