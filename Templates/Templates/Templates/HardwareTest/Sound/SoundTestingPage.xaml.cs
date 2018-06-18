using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.HardwareTest.Sound
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoundTestingPage : ContentPage
    {
        private static readonly IAudioService AudioServiceService = DependencyService.Get<IAudioService>();

        public SoundTestingPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AudioServiceService.StopPlaying();
        }

        private void PlayButton_OnClicked(object sender, EventArgs e)
        {
            if (PlayButton.Text == "Play")
            {
                AudioServiceService.PlayAudioFile("testSound.mp3");
                PlayButton.Text = "Stop";
            }
            else
            {
                AudioServiceService.StopPlaying();
                PlayButton.Text = "Play";
            }
        }
    }
}