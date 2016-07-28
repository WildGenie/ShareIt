using System.Windows.Controls;

namespace ShareIt.Pages.Settings
{
    /// <summary>
    ///     Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class BasicSettings : UserControl
    {
        public BasicSettings()
        {
            InitializeComponent();
            DataContext = new BasicShareItSettingsViewModel();
        }
    }
}
