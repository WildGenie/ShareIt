using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShareIt.Common;
using ShareIt.Properties;

namespace ShareIt.Pages.Settings
{
    public class BasicShareItettings
    {
        public string VideoDirectory { get; set; }
        public string ImageDirectory { get; set; }
    }

    public class BasicShareItSettingsViewModel : INotifyPropertyChanged
    {
        private readonly SerializableSettings<BasicShareItettings> _shareItSettingsModel;

        private string _imageDirectory;
        private string _videoDirectory;

        public BasicShareItSettingsViewModel()
        {
            _shareItSettingsModel = new SerializableSettings<BasicShareItettings>();
            _shareItSettingsModel.OnSettingsChanged += SettingChanged;
            _shareItSettingsModel.Load();
        }

        public string ImageDirectory
        {
            get { return _imageDirectory; }
            set
            {
                if (value == _imageDirectory)
                {
                    return;
                }
                _imageDirectory = value;
                _shareItSettingsModel.Current.ImageDirectory = value;
                OnPropertyChanged();
            }
        }

        public string VideoDirectory
        {
            get { return _videoDirectory; }
            set
            {
                if (value == _videoDirectory)
                {
                    return;
                }
                _videoDirectory = value;
                _shareItSettingsModel.Current.VideoDirectory = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveSettingsCommand => new DelegateCommand(SaveSettings);
        public ICommand LoadSettingsCommand => new DelegateCommand(LoadSettings);

        public event PropertyChangedEventHandler PropertyChanged;

        private void SettingChanged(object sender, string s)
        {
            var senderr = (ISettings<BasicShareItettings>) sender;
            var current = senderr.Current;
            ImageDirectory = current.ImageDirectory;
            VideoDirectory = current.VideoDirectory;
        }

        private void LoadSettings()
        {
            _shareItSettingsModel.Load();
        }

        private void SaveSettings()
        {
            _shareItSettingsModel.Current.ImageDirectory = ImageDirectory;
            _shareItSettingsModel.Current.VideoDirectory = VideoDirectory;
            _shareItSettingsModel.Save();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
