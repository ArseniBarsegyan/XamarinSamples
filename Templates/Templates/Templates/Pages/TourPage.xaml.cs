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
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void TourView_OnStartButtonClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}