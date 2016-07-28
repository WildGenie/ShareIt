using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ShareIt.Common;
using ShareIt.Common.Bitmaps;
using ShareIt.Properties;

namespace ShareIt.Pages.Animations
{
    public class GifRecordingSettings
    {
        public int Frames { get; set; } = 20;
        public int FrameRate { get; set; } = 5;
    }

    public class GifRecorderViewModel : INotifyPropertyChanged
    {
        private readonly BitmapScreenCapture _capture = new BitmapScreenCapture();
        private readonly ISettings<GifRecordingSettings> _settings = new SerializableSettings<GifRecordingSettings>();
        private int _frameRate;
        private int _frames;

        private GifBitmapEncoder _gifBitmapEncoder;
        private RelativeFile _path;

        public GifRecorderViewModel()
        {
            _capture.ScreenCaptured += CaptureOnScreenCaptured;
            _settings.Load();
            Frames = _settings.Current.Frames;
            FrameRate = _settings.Current.FrameRate;
        }

        public DelegateCommand RecordGifCommand => new DelegateCommand(RecordGif);

        public int Frames
        {
            get { return _frames; }
            set
            {
                if (value == _frames)
                {
                    return;
                }

                _frames = value;
                OnPropertyChanged();
            }
        }

        public int FrameRate
        {
            get { return _frameRate; }
            set
            {
                if (value == _frameRate)
                {
                    return;
                }

                _frameRate = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // WinForms example (it works exactly the same for WPF).
        private async void RecordGif()
        {
            // Since we asynchronously wait, the UI thread is not blocked by the file download.
            await RecordGifAsync();

            // Since we resume on the UI context, we can directly access UI elements.
            MessageBox.Show("Gif recording complete. It was saved to: ." + _path);
        }

        private Task RecordGifAsync()
        {
            return Task.Run(() => { RecordGif(Frames, FrameRate/1000); });
        }

        private void RecordGif(int frames, int interval)
        {
            _gifBitmapEncoder = new GifBitmapEncoder();

            for (var i = 0; i < frames; i++)
            {
                _capture.CaptureScreen();
                Thread.Sleep(interval);
            }

            _path = new RelativeFile
            {
                SubDirectory = "Animations",
                Name = "GifRecording",
                Extension = ".gif",
                TimeStampFile = true,
                UseDetailedTimeStamp = true
            };

            using (var filestream = new FileStream(_path.GetFullPath(), FileMode.Create))
            {
                _gifBitmapEncoder.Save(filestream);
            }

            _gifBitmapEncoder = null;
        }

        private void CaptureOnScreenCaptured(object sender, Bitmap bitmap)
        {
            if (_gifBitmapEncoder != null)
            {
                var frame = BitmapFrame.Create(bitmap.ToSafeSource().Instance);
                _gifBitmapEncoder.Frames.Add(frame);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
