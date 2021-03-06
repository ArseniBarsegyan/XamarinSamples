﻿using System;
using System.Threading.Tasks;
using Templates.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertsDemoPage : ContentPage
    {
        private readonly IAlertService _alertService = DependencyService.Get<IAlertService>();
        private readonly ILoadingService _loadingService = DependencyService.Get<ILoadingService>();
        private readonly IPermissionService _permissionService = DependencyService.Get<IPermissionService>();
        private readonly IDeviceOrientation _deviceOrientationService = DependencyService.Get<IDeviceOrientation>();

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

        private async void ShowLoadingButton_OnClicked(object sender, EventArgs e)
        {
            _loadingService.ShowLoading();
            await Task.Delay(2000);
            _loadingService.HideLoading();
        }

        private async void ShowLoadingWithMessageButton_OnClicked(object sender, EventArgs e)
        {
            _loadingService.ShowLoading("Loading with message");
            await Task.Delay(2000);
            _loadingService.HideLoading();
        }

        private async void RequestPermissionButton_OnClicked(object sender, EventArgs e)
        {
            var result = await _permissionService.AskPermission();
            if (result)
            {
                _alertService.ShowOkAlert("Permission GRANTED!", "Ok");
            }
            else
            {
                _alertService.ShowOkAlert("Permission DENIED!", "Ok");
            }
        }

        private void GetOrientationButton_OnClicked(object sender, EventArgs e)
        {
            _alertService.ShowOkAlert("Curren orientation is: " + _deviceOrientationService.GetOrientation(), "Ok");
        }
    }
}