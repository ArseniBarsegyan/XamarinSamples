using Xamarin.Forms;
using System.IO;
using Foundation;
using AVFoundation;
using Templates.HardwareTest.Sound;
using Templates.iOS.HardwareTest.Sound;

[assembly: Dependency(typeof(AudioService))]
namespace Templates.iOS.HardwareTest.Sound
{
    public class AudioService : NSObject, IAudioService, IAVAudioPlayerDelegate
    {
        private AVAudioPlayer _player;

        public void PlayAudioFile(string fileName)
        {
            NSError error = null;
            AVAudioSession.SharedInstance().SetCategory(AVAudioSession.CategoryPlayback, out error);

            var sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            var url = NSUrl.FromString(sFilePath);
            _player = AVAudioPlayer.FromUrl(url);
            _player.Delegate = this;
            _player.Volume = 100f;
            _player.NumberOfLoops = -1;
            _player.PrepareToPlay();
            _player.FinishedPlaying += (sender, e) =>
            {
                _player = null;
            };
            _player.Play();
        }

        public void StopPlaying()
        {
            _player?.Stop();
        }
    }
}