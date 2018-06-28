using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using BottomBarDemoApp01.BottomBar;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using BottomBarDemoApp01.Droid.BottomBar;
using BottomBarDemoApp01.Droid.BottomBar.Util;
using BottomBarDemoApp01.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using IPageController = BottomBar.Droid.IPageController;
using PageController = BottomBar.Droid.PageController;

[assembly: ExportRenderer(typeof(BottomBarPage), typeof(CustomBottomBarRenderer))]
namespace BottomBarDemoApp01.Droid.BottomBar
{
    public class CustomBottomBarRenderer : VisualElementRenderer<BottomBarPage>, IOnTabClickListener
    {
        private bool _disposed;
        private BottomNavigationBar.BottomBar _bottomBar;
        private FrameLayout _frameLayout;
        private IPageController _pageController;

        public CustomBottomBarRenderer()
        {
            this.AutoPackage = false;
        }

        public void OnTabSelected(int position)
        {
            this.SwitchContent(this.Element.Children[position]);
            this.Element.CurrentPage = this.Element.Children[position];
        }

        public void OnTabReSelected(int position)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this._disposed)
            {
                this._disposed = true;
                this.RemoveAllViews();
                foreach (VisualElement child in (IEnumerable<Page>)this.Element.Children)
                {
                    IVisualElementRenderer renderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(child);
                    if (renderer != null)
                    {
                        renderer.ViewGroup.RemoveFromParent();
                        renderer.Dispose();
                    }
                }
                if (this._bottomBar != null)
                {
                    this._bottomBar.SetOnTabClickListener((IOnTabClickListener)null);
                    this._bottomBar.Dispose();
                    this._bottomBar = (BottomNavigationBar.BottomBar)null;
                }
                if (this._frameLayout != null)
                {
                    this._frameLayout.Dispose();
                    this._frameLayout = (FrameLayout)null;
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            this._pageController.SendAppearing();
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            this._pageController.SendDisappearing();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BottomBarPage> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;
            BottomBarPage newElement = e.NewElement;
            if (this._bottomBar == null)
            {
                this._pageController = PageController.Create((Page)newElement);
                this._frameLayout = new FrameLayout(Xamarin.Forms.Forms.Context)
                {
                    LayoutParameters = (ViewGroup.LayoutParams)new FrameLayout.LayoutParams(-1, -1, GravityFlags.Fill)
                };
                this.AddView((Android.Views.View)this._frameLayout, 0);
                this._bottomBar = BottomNavigationBar.BottomBar.Attach((Android.Views.View)this._frameLayout, (Bundle)null);
                this._bottomBar.NoTabletGoodness();
                if (newElement.FixedMode)
                    this._bottomBar.UseFixedMode();
                switch (newElement.BarTheme)
                {
                    case BottomBarPage.BarThemeTypes.Light:
                        this._bottomBar.LayoutParameters = new ViewGroup.LayoutParams(-1, -1);
                        this._bottomBar.SetOnTabClickListener((IOnTabClickListener)this);
                        this.UpdateTabs();
                        this.UpdateBarBackgroundColor();
                        this.UpdateBarTextColor();
                        _bottomBar.SelectTabAtPosition(Element.Children.IndexOf(Element.CurrentPage), true);
                        break;
                    case BottomBarPage.BarThemeTypes.DarkWithAlpha:
                        this._bottomBar.UseDarkThemeWithAlpha(true);
                        goto case BottomBarPage.BarThemeTypes.Light;
                    case BottomBarPage.BarThemeTypes.DarkWithoutAlpha:
                        this._bottomBar.UseDarkThemeWithAlpha(false);
                        goto case BottomBarPage.BarThemeTypes.Light;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            if (newElement.CurrentPage == null)
                return;
            this.SwitchContent(newElement.CurrentPage);

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "CurrentPage")
            {
                this.SwitchContent(this.Element.CurrentPage);
                this.UpdateSelectedTabIndex(this.Element.CurrentPage);
            }
            else if (e.PropertyName == NavigationPage.BarBackgroundColorProperty.PropertyName)
            {
                this.UpdateBarBackgroundColor();
            }
            else
            {
                if (e.PropertyName != NavigationPage.BarTextColorProperty.PropertyName)
                    return;
                this.UpdateBarTextColor();
            }

        }

        protected virtual void SwitchContent(Page view)
        {
            this.Context.HideKeyboard((Android.Views.View)this);
            this._frameLayout.RemoveAllViews();
            if (view == null)
                return;
            if (Xamarin.Forms.Platform.Android.Platform.GetRenderer((VisualElement)view) == null)
                Xamarin.Forms.Platform.Android.Platform.SetRenderer((VisualElement)view, Xamarin.Forms.Platform.Android.Platform.CreateRenderer((VisualElement)view));
            this._frameLayout.AddView((Android.Views.View)Xamarin.Forms.Platform.Android.Platform.GetRenderer((VisualElement)view).ViewGroup);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            int num1 = r - l;
            int num2 = b - t;
            Context context = this.Context;
            this._bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(num1, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(num2, MeasureSpecMode.AtMost));
            int num3 = Math.Min(num2, Math.Max(this._bottomBar.MeasuredHeight, this._bottomBar.MinimumHeight));
            if (num1 > 0 && num2 > 0)
            {
                this._pageController.ContainerArea = new Rectangle(0.0, 0.0, context.FromPixels((double)num1), context.FromPixels((double)this._frameLayout.MeasuredHeight));
                ObservableCollection<Element> internalChildren = this._pageController.InternalChildren;
                for (int index = 0; index < internalChildren.Count; ++index)
                {
                    VisualElement bindable = internalChildren[index] as VisualElement;
                    if (bindable != null)
                    {
                        NavigationPageRenderer renderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(bindable) as NavigationPageRenderer;
                    }
                }
                this._bottomBar.Measure(MeasureSpecFactory.MakeMeasureSpec(num1, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(num3, MeasureSpecMode.Exactly));
                this._bottomBar.Layout(0, 0, num1, num3);
            }
            base.OnLayout(changed, l, t, r, b);
        }

        private void UpdateSelectedTabIndex(Page page)
        {
            this._bottomBar.SelectTabAtPosition(this.Element.Children.IndexOf(page), true);
        }

        private void UpdateBarBackgroundColor()
        {
            if (this._disposed || this._bottomBar == null)
                return;
            this._bottomBar.SetBackgroundColor(this.Element.BarBackgroundColor.ToAndroid());
        }

        private void UpdateBarTextColor()
        {
            if (this._disposed || this._bottomBar == null)
                return;
            this._bottomBar.SetActiveTabColor(this.Element.BarTextColor.ToAndroid());
        }

        private void UpdateTabs()
        {
            this.SetTabItems();
            this.SetTabColors();
        }

        private void SetTabItems()
        {
            BottomBarTab[] array = this.Element.Children.Select<Page, BottomBarTab>((Func<Page, BottomBarTab>)(page => new BottomBarTab(ResourceManagerEx.IdFromTitle((string)page.Icon, ResourceManager.DrawableClass), page.Title))).ToArray<BottomBarTab>();
            if (array.Length == 0)
                return;
            this._bottomBar.SetItems(array);
        }

        private void SetTabColors()
        {
            for (int tabPosition = 0; tabPosition < this.Element.Children.Count; ++tabPosition)
            {
                Xamarin.Forms.Color tabColor = BottomBarPageExtensions.GetTabColor((BindableObject)this.Element.Children[tabPosition]);
                if (tabColor != Xamarin.Forms.Color.Transparent)
                {
                    this._bottomBar.MapColorForTab(tabPosition, tabColor.ToAndroid());
                }
            }
        }
    }
}