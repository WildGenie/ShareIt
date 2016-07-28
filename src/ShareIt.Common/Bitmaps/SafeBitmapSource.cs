using System;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace ShareIt.Common.Bitmaps
{
    /// <summary>
    /// </summary>
    public class SafeBitmapSource : IDisposable
    {
        /// <summary>
        ///     The _disposed
        /// </summary>
        private volatile bool _disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SafeBitmapSource" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="hBitmap">The h bitmap.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SafeBitmapSource(BitmapSource source, IntPtr hBitmap)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Instance = source;
            HBitmap = hBitmap;
        }

        /// <summary>
        ///     Gets the source.
        /// </summary>
        /// <value>
        ///     The source.
        /// </value>
        public BitmapSource Instance { get; private set; }

        /// <summary>
        ///     Gets the h bitmap.
        /// </summary>
        /// <value>
        ///     The h bitmap.
        /// </value>
        public IntPtr HBitmap { get; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                DeleteObject(HBitmap);
                _disposed = true;
            }
        }

        /// <summary>
        ///     Deletes the object.
        /// </summary>
        /// <param name="hObject">The h object.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
    }
}
