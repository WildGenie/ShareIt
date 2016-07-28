using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ShareIt.Common.Bitmaps
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class DirectBitmap : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectBitmap" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width*height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width*4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        /// <summary>
        ///     Gets the bitmap.
        /// </summary>
        /// <value>
        ///     The bitmap.
        /// </value>
        public Bitmap Bitmap { get; }

        /// <summary>
        ///     Gets the bits.
        /// </summary>
        /// <value>
        ///     The bits.
        /// </value>
        public int[] Bits { get; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="DirectBitmap" /> is disposed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        public bool Disposed { get; private set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        ///     Gets the bits handle.
        /// </summary>
        /// <value>
        ///     The bits handle.
        /// </value>
        protected GCHandle BitsHandle { get; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Disposed)
            {
                return;
            }
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
