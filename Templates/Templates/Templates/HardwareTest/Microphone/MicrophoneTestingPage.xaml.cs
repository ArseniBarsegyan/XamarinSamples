using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.HardwareTest.Microphone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MicrophoneTestingPage : ContentPage
    {
        private static readonly IMicrophoneService MicrophoneService = DependencyService.Get<IMicrophoneService>();

        public MicrophoneTestingPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MicrophoneService.StopRecording();
            MicrophoneService.StopPlaying();
        }

        private void ControlButton_OnClicked(object sender, EventArgs e)
        {
            switch (ControlButton.Text)
            {
                case "Record":
                    MicrophoneService.StopPlaying();
                    MicrophoneService.StartRecording("voiceTest");
                    ControlButton.Text = "Stop recording";
                    break;

                case "Stop recording":
                    MicrophoneService.StopRecording();
                    ControlButton.Text = "Play";
                    break;

                case "Play":
                    MicrophoneService.PlayRecordedSound();
                    ControlButton.Text = "Record";
                    break;
            }
        }
    }
}