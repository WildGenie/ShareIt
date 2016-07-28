using System;
using System.Drawing;

namespace ShareIt.Common.Bitmaps
{
    /// <summary>
    /// </summary>
    public class BitmapScreenCapture : IScreenCapture<Bitmap>
    {
        private DirectBitmap _directBitmap;
        private ScreenRegion _screenRegion;

        /// <summary>
        ///     Gets or sets the screen region.
        /// </summary>
        /// <value>
        ///     The screen region.
        /// </value>
        public ScreenRegion ScreenRegion
        {
            get { return _screenRegion ?? (_screenRegion = ScreenRegion.GetFullScreen()); }
            set { _screenRegion = value; }
        }

        public void CaptureScreen()
        {
            ValidateDirectBitmap();
            var screen = GetScreenBitmap();
            OnScreepCaptured(screen);
        }

        /// <summary>
        ///     Occurs when [screep captured].
        /// </summary>
        public event EventHandler<Bitmap> ScreenCaptured;

        /// <summary>
        ///     Called when [screep captured].
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        protected virtual void OnScreepCaptured(Bitmap bitmap)
        {
            ScreenCaptured?.Invoke(this, bitmap);
        }

        private Bitmap GetScreenBitmap()
        {
            CopyScreenToGraphics(_directBitmap.Bitmap.ToGraphics());
            return _directBitmap.Bitmap;
        }

        private void CopyScreenToGraphics(Graphics screen)
        {
            screen.CopyFromScreen(ScreenRegion.UpperLeftCorner.X, ScreenRegion.UpperLeftCorner.Y, 0, 0,
                new Size(_directBitmap.Width, _directBitmap.Height));
        }

        private void ValidateDirectBitmap()
        {
            var width = ScreenRegion.LowerRightCorner.X - ScreenRegion.UpperLeftCorner.X;
            var height = ScreenRegion.LowerRightCorner.Y - ScreenRegion.UpperLeftCorner.Y;

            if (_directBitmap == null)
            {
                _directBitmap = new DirectBitmap(width, height);
            }

            else if (_directBitmap.Width != width)
            {
                _directBitmap = new DirectBitmap(width, height);
            }

            else if (_directBitmap.Height != height)
            {
                _directBitmap = new DirectBitmap(width, height);
            }
        }
    }
}
