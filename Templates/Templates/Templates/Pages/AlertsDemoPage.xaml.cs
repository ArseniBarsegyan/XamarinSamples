using System;
using Templates.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertsDemoPage : ContentPage
    {
        private IAlertService _alertService = DependencyService.Get<IAlertService>();

        public AlertsDemoPage()
        {
            InitializeComponent();
        }

        private void AlertButton_OnClicked(object sender, EventArgs e)
        {
            _alertService.ShowAlert("Custom alert");
        }

        private async void ResolutionAlertButton_OnClicked(object sender, EventArgs e)
        {
            await _alertService.ShowResolution("Resolution alert");
        }

        private void OkAlertButton_OnClicked(object sender, EventArgs e)
        {
            _alertService.ShowOkAlert("Ok alert", "Ok");
        }

        private void UploadAlertButton_OnClicked(object sender, EventArgs e)
        {
            _alertService.ShowUploadAlert("Upload alert", "Ok");
        }

        private void YesNoAlertButton_OnClicked(object sender, EventArgs e)
        {
            _alertService.ShowYesNoAlert("Yes/No alert", "Yes", "No");
        }

        private void WarningAlertButton_OnClicked(object sender, EventArgs e)
        {
            _alertService.ShowWarningAlert("Warning", "Ok");
        }
    }
}