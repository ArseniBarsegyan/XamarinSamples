using Xamarin.Forms;

namespace BottomBarDemoApp01.BottomBar
{
    public class BottomBarPage : TabbedPage
    {
        public bool FixedMode { get; set; }

        public BottomBarPage.BarThemeTypes BarTheme { get; set; }

        public void RaiseCurrentPageChanged()
        {
            OnCurrentPageChanged();
        }

        public enum BarThemeTypes
        {
            Light,
            DarkWithAlpha,
            DarkWithoutAlpha,
        }
    }
}