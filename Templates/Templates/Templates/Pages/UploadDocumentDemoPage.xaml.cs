using System;
using Templates.Services;
using Templates.Services.DocumentPickerService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadDocumentDemoPage : ContentPage
    {
        private static readonly IPlatformDocumentPicker DocumentPicker = DependencyService.Get<IPlatformDocumentPicker>();
        private static readonly IFileSystem FileService = DependencyService.Get<IFileSystem>();
        private static readonly IAlertService AlertService = DependencyService.Get<IAlertService>();

        public UploadDocumentDemoPage()
        {
            InitializeComponent();
        }

        private async void UploadButton_OnClicked(object sender, EventArgs e)
        {
            var document = await DocumentPicker.DisplayImportAsync(this);

            if (document == null)
            {
                return;
            }

            // Retrieve file content throught IFileService implementation.
            byte[] fileContent = FileService.ReadAllBytes(document.Path);
            AlertService.ShowOkAlert("Successfully retrieved file content and file name", "Ok");
            FileName.Text = document.Name;
        }
    }
}