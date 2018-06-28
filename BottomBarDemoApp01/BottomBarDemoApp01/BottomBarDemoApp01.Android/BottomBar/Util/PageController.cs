using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BottomBarDemoApp01.Droid.BottomBar.Util
{
    public class PageController : IPageController
    {
        private ReflectedProxy<Page> _proxy;

        public static IPageController Create(Page page)
        {
            return (IPageController)new PageController(page);
        }

        private PageController(Page page)
        {
            this._proxy = new ReflectedProxy<Page>(page);
        }

        public Rectangle ContainerArea
        {
            get => this._proxy.GetPropertyValue<Rectangle>(nameof(ContainerArea));
            set => this._proxy.SetPropertyValue((object)value, nameof(ContainerArea));
        }

        public bool IgnoresContainerArea
        {
            get => this._proxy.GetPropertyValue<bool>(nameof(IgnoresContainerArea));
            set => this._proxy.SetPropertyValue((object)value, nameof(IgnoresContainerArea));
        }

        public ObservableCollection<Element> InternalChildren
        {
            get => this._proxy.GetPropertyValue<ObservableCollection<Element>>(nameof(InternalChildren));
            set => this._proxy.SetPropertyValue((object)value, nameof(InternalChildren));
        }

        public void SendAppearing()
        {
            this._proxy.Call(nameof(SendAppearing), (object[])null);
        }

        public void SendDisappearing()
        {
            this._proxy.Call(nameof(SendDisappearing), (object[])null);
        }
    }
}