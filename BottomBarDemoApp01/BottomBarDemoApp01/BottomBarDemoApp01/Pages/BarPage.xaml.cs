using System.Linq;
using BottomBarDemoApp01.BottomBar;
using Xamarin.Forms.Xaml;

namespace BottomBarDemoApp01.Pages
{
    /// <summary>
    /// Extension of xamarin forms TabbedPage using BottomNavigationBar,
    /// since Android normaly puts Tabbs at the top of the page and iOS at the bottom.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarPage : BottomBarPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarPage"/> class.
        /// Also sets the color of the tabs.
        /// </summary>
        /// <param name="index">index of page to be displayed first.</param>
		public BarPage(int index)
        {
            InitializeComponent();
            if (Children != null && Children?.Count != 0 && index >= 0 && index != Children.IndexOf(CurrentPage))
            {
                CurrentPage = Children[index];
            }
            Title = CurrentPage.Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage.Title;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Navigation.NavigationStack != null && Navigation.NavigationStack.Count > 1)
            {
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    if (page != this)
                        Navigation.RemovePage(page);
                }
            }
        }
    }
}