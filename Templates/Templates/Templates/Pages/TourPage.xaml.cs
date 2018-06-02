using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.Pages
{
    /// <summary>
    /// Demo page for CarouselView.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TourPage : ContentPage
    {
        public TourPage()
        {
            InitializeComponent();
        }

        private async void TourView_OnStartButtonClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}