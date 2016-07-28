using System.Windows;
using Point = System.Drawing.Point;

namespace ShareIt.Common
{
    /// <summary>
    ///     Represents the screen region class.
    /// </summary>
    public class ScreenRegion
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScreenRegion" /> class.
        ///     Used to select a screen region for animations, screenshots and videos.
        ///     Use: SetUpperLeftCorner(), SetLowerRightCorner().
        /// </summary>
        public ScreenRegion(Point upperleft, Point lowerright)
        {
            UpperLeftCorner = upperleft;
            LowerRightCorner = lowerright;
        }

        /// <summary>
        ///     Gets or sets the upper left corner position.
        /// </summary>
        public Point UpperLeftCorner { get; set; }

        /// <summary>
        ///     Gets or sets the lower right corner position.
        /// </summary>
        public Point LowerRightCorner { get; set; }

        /// <summary>
        ///     Gets the screen region of the full primary screen.
        /// </summary>
        /// <returns></returns>
        public static ScreenRegion GetFullScreen()
        {
            var width = SystemParameters.PrimaryScreenWidth;
            var height = SystemParameters.PrimaryScreenHeight;
            return new ScreenRegion(new Point(0, 0), new Point((int) width, (int) height));
        }
    }
}
