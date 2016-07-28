using System;

namespace ShareIt.Common
{
    /// <summary>
    ///     Interface defining a way to capture a screen.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IScreenCapture<T>
    {
        /// <summary>
        ///     Gets or sets the screen region.
        /// </summary>
        /// <value>
        ///     The screen region.
        /// </value>
        ScreenRegion ScreenRegion { get; set; }

        /// <summary>
        ///     Captures the screen.
        /// </summary>
        /// <returns></returns>
        void CaptureScreen();

        /// <summary>
        ///     Occurs when [screep captured].
        /// </summary>
        event EventHandler<T> ScreenCaptured;
    }
}
