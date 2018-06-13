using System;
using System.Linq;
using Templates.Elements;
using Templates.HardwareTest.Camera;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : MasterDetailPage, IDisposable
    {
        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var pages = MenuHelper.GetMenu().Where(x => x.IsDisplayed).ToList();
            MenuList.ItemsSource = pages;

            MessagingCenter.Subscribe<ControlsDemoPage, MenuPageIndex>(this, "Detail page changed", (sender, pageIndex) =>
            {
                switch (pageIndex)
                {
                    case MenuPageIndex.ControlsPage:
                        Navigation.PushAsync(new ControlsDemoPage());
                        break;
                    case MenuPageIndex.ElementsPage:
                        Navigation.PushAsync(new ElementsDemoPage());
                        break;
                    case MenuPageIndex.TourPage:
                        Navigation.PushAsync(new TourPage());
                        break;
                    case MenuPageIndex.AlertsPage:
                        Navigation.PushAsync(new AlertsDemoPage());
                        break;
                    case MenuPageIndex.UploadPage:
                        Navigation.PushAsync(new UploadDocumentDemoPage());
                        break;
                    case MenuPageIndex.CameraTestPage:
                        Navigation.PushAsync(new CameraPreviewPage());
                        break;
                    default:
                        break;
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Navigation.NavigationStack.Count > 0)
            {
                var pages = Navigation.NavigationStack;
                for (int i = 0; i < pages.Count; i++)
                {
                    if (pages[i] != this)
                    {
                        Navigation.RemovePage(pages[i]);
                    }
                }
            }
        }

        private void MenuList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterPageItem item)
            {
                MenuList.SelectedItem = null;
                if (item.TargetType != typeof(string))
                {
                    var page = Activator.CreateInstance(item.TargetType) as Page;
                    NavigateTo(page);
                }
            }
        }

        public void NavigateTo(Page page)
        {
            Detail = new NavigationPage(page);
            IsPresented = false;
        }

        public void Dispose()
        {
            MessagingCenter.Unsubscribe<ControlsDemoPage, MenuPageIndex>(this, "Detail page changed");
        }
    }
}