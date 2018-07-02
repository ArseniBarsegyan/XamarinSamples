using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BottomBarDemoApp02
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : TabbedPage, IDisposable
    {
        private static TabPage _instance;

        public TabPage()
        {
            InitializeComponent();
        }

        public static TabPage Instance => _instance ?? (_instance = new TabPage());
        public bool GotoTabFlag { get; set; }
        public bool IsNotification { get; set; }

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
        
        public async void GoToTab(int tabIndex, bool isNotification = false)
        {
            IsNotification = isNotification;
            GotoTabFlag = true;
            await Application.Current.MainPage.Navigation.PopToRootAsync(true);
            var tabbedPage = this as TabbedPage;
            var index = (tabIndex > tabbedPage.Children.Count - 1) ? tabbedPage.Children.Count - 1 : ((tabIndex < 0) ? 0 : tabIndex);
            tabbedPage.CurrentPage = tabbedPage.Children[index];
            GotoTabFlag = false;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public void Dispose()
        {
            _instance = null;
        }
    }
}