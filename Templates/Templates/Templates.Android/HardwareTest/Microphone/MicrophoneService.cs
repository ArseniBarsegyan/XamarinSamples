using System.IO;
using Android.Content;
using Android.Media;
using Android.Net;
using Plugin.CurrentActivity;
using Templates.Droid.HardwareTest.Microphone;
using Templates.HardwareTest.Microphone;
using Xamarin.Forms;

[assembly: Dependency(typeof(MicrophoneService))]
namespace Templates.Droid.HardwareTest.Microphone
{
    /// <summary>
    /// Implementaion of <see cref="IMicrophoneService" />.
    /// </summary>
    public class MicrophoneService : IMicrophoneService
    {
        private MediaRecorder _recorder;
        private MediaPlayer _mediaPlayer;

        // file name including extension
        private string _fileName;
        // Full path to recorded file, including file name and file extension
        private string _fullFilePath;
        private readonly Context _context = CrossCurrentActivity.Current.Activity.ApplicationContext;

        public void StartRecording(string fileName)
        {
            _fileName = fileName + ".3gpp";
            _recorder = new MediaRecorder();
            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.ThreeGpp);

            var outputDirectory = _context.GetExternalFilesDir(Android.OS.Environment.DirectoryMusic).AbsolutePath;

            _recorder.SetOutputFile(Path.Combine(outputDirectory, _fileName));
            _fullFilePath = Path.Combine(outputDirectory, _fileName);

            _recorder.SetAudioEncoder(AudioEncoder.AmrNb);

            _recorder.Prepare();
            _recorder.Start();
        }

        public void StopRecording()
        {
            if (_recorder == null)
            {
                return;
            }
            _recorder.Stop();
            _recorder.Release();
            _recorder = null;
        }

        public void PlayRecordedSound()
        {
            if (_fullFilePath == null)
            {
                return;
            }

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Prepared += (sender, args) =>
            {
                _mediaPlayer.Start();
            };
            _mediaPlayer.SetDataSource(_context, Uri.Parse(_fullFilePath));
            _mediaPlayer.Prepare();
        }

        public void StopPlaying()
        {
            _mediaPlayer?.Stop();
        }
    }
}