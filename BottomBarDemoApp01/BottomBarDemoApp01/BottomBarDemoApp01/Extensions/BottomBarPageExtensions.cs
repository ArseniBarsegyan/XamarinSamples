using Xamarin.Forms;

namespace BottomBarDemoApp01.Extensions
{
    public class BottomBarPageExtensions
    {
        public static readonly BindableProperty TabColorProperty = BindableProperty.CreateAttached("TabColor", typeof(Color), typeof(BottomBarPageExtensions), (object)Color.Transparent, BindingMode.OneWay, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        public static void SetTabColor(BindableObject bindable, Color color)
        {
            bindable.SetValue(BottomBarPageExtensions.TabColorProperty, (object)color);
        }

        public static Color GetTabColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(BottomBarPageExtensions.TabColorProperty);
        }
    }
}