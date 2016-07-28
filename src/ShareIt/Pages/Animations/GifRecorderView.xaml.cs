using System.Windows.Controls;

namespace ShareIt.Pages.Animations
{
    /// <summary>
    ///     Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class GifRecorderView : UserControl
    {
        public GifRecorderView()
        {
            InitializeComponent();
            DataContext = new GifRecorderViewModel();
        }
    }
}
