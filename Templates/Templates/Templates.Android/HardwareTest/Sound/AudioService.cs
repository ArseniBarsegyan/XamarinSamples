using Xamarin.Forms;
using Android.Media;
using Templates.Droid.HardwareTest.Sound;
using Templates.HardwareTest.Sound;

[assembly: Dependency(typeof(AudioService))]
namespace Templates.Droid.HardwareTest.Sound
{
    public class AudioService : IAudioService
    {
        private MediaPlayer _mediaPlayer;

        public void PlayAudioFile(string fileName)
        {
            _mediaPlayer = new MediaPlayer { Looping = true };
            var fd = Android.App.Application.Context.Assets.OpenFd(fileName);

            _mediaPlayer.Prepared += (s, e) =>
            {
                _mediaPlayer.Start();
            };
            _mediaPlayer.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            _mediaPlayer.Prepare();
        }

        public void StopPlaying()
        {
            _mediaPlayer?.Stop();
        }
    }
}