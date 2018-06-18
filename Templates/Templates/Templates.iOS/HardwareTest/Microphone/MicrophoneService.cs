using System;
using System.IO;
using AVFoundation;
using Foundation;
using Templates.HardwareTest.Microphone;
using Templates.iOS.HardwareTest.Microphone;
using Xamarin.Forms;
[assembly: Dependency(typeof(MicrophoneService))]
namespace Templates.iOS.HardwareTest.Microphone
{
    /// <summary>
    /// Implementation of <see cref="IMicrophoneService" />.
    /// </summary>
    public class MicrophoneService : NSObject, IMicrophoneService, IAVAudioPlayerDelegate
    {
        private AVAudioRecorder _recorder;
        private AVAudioPlayer _player;
        private NSUrl _url;
        private NSDictionary _settings;
        // file name including extension
        private string _fileName;
        private string _fullFilePath;

        private void InitializeAudioSession()
        {
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return;
            }
            err = audioSession.SetActive(true);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return;
            }
        }

        public void StartRecording(string fileName)
        {
            InitializeAudioSession();

            _fileName = fileName + ".wav";
            _fullFilePath = Path.Combine(Path.GetTempPath(), _fileName);

            Console.WriteLine("Audio File Path: " + _fullFilePath);

            _url = NSUrl.FromFilename(_fullFilePath);
            //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
            NSObject[] values =
            {
                NSNumber.FromFloat (44100.0f), //Sample Rate
                NSNumber.FromInt32 ((int)AudioToolbox.AudioFormatType.LinearPCM), //AVFormat
                NSNumber.FromInt32 (2), //Channels
                NSNumber.FromInt32 (16), //PCMBitDepth
                NSNumber.FromBoolean (false), //IsBigEndianKey
                NSNumber.FromBoolean (false) //IsFloatKey
            };

            //Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
            NSObject[] keys =
            {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVLinearPCMBitDepthKey,
                AVAudioSettings.AVLinearPCMIsBigEndianKey,
                AVAudioSettings.AVLinearPCMIsFloatKey
            };

            //Set Settings with the Values and Keys to create the NSDictionary
            _settings = NSDictionary.FromObjectsAndKeys(values, keys);

            //Set recorder parameters
            _recorder = AVAudioRecorder.Create(_url, new AudioSettings(_settings), out _);
            _recorder.PrepareToRecord();
            _recorder.Record();
        }

        public void StopRecording()
        {
            if (_recorder == null)
            {
                return;
            }
            _recorder.Stop();
            _recorder = null;
        }

        public void PlayRecordedSound()
        {
            if (_fullFilePath == null)
            {
                return;
            }

            NSError error = null;
            AVAudioSession.SharedInstance().SetCategory(AVAudioSession.CategoryPlayback, out error);

            var url = NSUrl.FromString(_fullFilePath);
            _player = AVAudioPlayer.FromUrl(url);
            _player.Delegate = this;
            _player.Volume = 100f;

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