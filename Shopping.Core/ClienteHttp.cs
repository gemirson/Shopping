using Microsoft.Win32.SafeHandles;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Shopping.Core
{
    public class ClienteHttp<T>: IDisposable
    {
        protected readonly IHttpClientFactory _httpClientFactory;

        public ClienteHttp(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
            IsHttp = true;
        }

        public string ClientName{ get; set; }

        public bool IsHttp { get;}

      

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }
       
    }
}
