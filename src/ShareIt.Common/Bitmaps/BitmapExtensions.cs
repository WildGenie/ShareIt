using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ShareIt.Common.Bitmaps
{
    /// <summary>
    ///     Class containing extension methods for bitmap extensions.
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        ///     Converts the bitmap to a <see cref="BitmapSource" />.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        public static BitmapSource ToSource(this Bitmap bitmap)
        {
            var hbitmap = bitmap.GetHbitmap();

            return Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        ///     Converts the bitmap to a <see cref="SafeBitmapSource" />, which is a wrapper for the <see cref="BitmapSource" />
        ///     class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        public static SafeBitmapSource ToSafeSource(this Bitmap bitmap)
        {
            var hbitmap = bitmap.GetHbitmap();

            var source = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return new SafeBitmapSource(source, hbitmap);
        }

        /// <summary>
        ///     Converts the bitmap to the <see cref="Graphics" /> from the image.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        public static Graphics ToGraphics(this Bitmap bitmap)
        {
            return Graphics.FromImage(bitmap);
        }
    }
}
