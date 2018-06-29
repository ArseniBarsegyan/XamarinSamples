using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using BottomBarDemoApp02;
using BottomBarDemoApp02.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TabPage), typeof(TabPageRenderer))]
namespace BottomBarDemoApp02.Droid
{
    public class TabPageRenderer : VisualElementRenderer<TabPage>, IOnTabClickListener
    {
        private BottomBar _bottomBar;

        private Page _currentPage;

        private int _lastSelectedTabIndex = -1;

        public TabPageRenderer(Context context) : base(context)
        {
            // Required to say packager to not to add child pages automatically
            AutoPackage = false;
        }

        public void OnTabSelected(int position)
        {
            LoadPageContent(position);
        }

        public void OnTabReSelected(int position)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabPage> e)
        {
            base.OnElementChanged(e);

            ClearElement();

            if (e.NewElement != null)
            {
                InitializeElement(e.NewElement);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "CurrentPage" && Element != null && Element.GotoTabFlag)
            {
                Element.GotoTabFlag = false;
                PopulateChildrenAndGoToTab(Element, Element.IsNotification);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearElement();
            }

            try
            {
                base.Dispose(disposing);

            }
            catch (Exception)
            {
                // ignored
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (Element == null)
            {
                return;
            }

            int width = r - l;
            int height = b - t;

            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.AtMost));

            // We need to call measure one more time with measured sizes 
            // in order to layout the bottom bar properly
            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(_bottomBar.ItemContainer.MeasuredHeight, MeasureSpecMode.Exactly));

            int barHeight = _bottomBar.ItemContainer.MeasuredHeight;

            _bottomBar.Layout(0, b - barHeight, width, b);

            float density = Android.Content.Res.Resources.System.DisplayMetrics.Density;

            double contentWidthConstraint = width / density;
            double contentHeightConstraint = (height - barHeight) / density;

            if (_currentPage != null)
            {
                var renderer = Platform.GetRenderer(_currentPage);

                renderer.Element.Measure(contentWidthConstraint, contentHeightConstraint);
                renderer.Element.Layout(new Rectangle(0, 0, contentWidthConstraint, contentHeightConstraint));

                renderer.UpdateLayout();
            }
        }

        private void InitializeElement(TabPage element)
        {
            PopulateChildren(element);
        }

        private void PopulateChildren(TabPage element)
        {
            // Unfortunately bottom bar can not be reused so we have to
            // remove it and create the new instance
            _bottomBar?.RemoveFromParent();

            _bottomBar = CreateBottomBar(element.Children);
            AddView(_bottomBar);

            LoadPageContent(0);
        }

        private void PopulateChildrenAndGoToTab(TabPage element, bool isNotification = false)
        {
            if (!isNotification)
            {
                _bottomBar?.RemoveFromParent();

                _bottomBar = CreateBottomBar(element.Children);
                AddView(_bottomBar);
            }

            for (int i = 0; i < element.Children.Count(); i++)
            {
                if (element.Children[i] == element.CurrentPage)
                {
                    _bottomBar?.SelectTabAtPosition(i, true);
                    LoadPageContent(i);
                    return;
                }
            }
        }

        private void ClearElement()
        {
            if (Element != null)
            {
                foreach (var elementChild in Element.Children)
                {
                    var rendererChild = Platform.GetRenderer(elementChild);
                    rendererChild?.View?.RemoveFromParent();
                    Platform.SetRenderer(elementChild, null);
                }
                if (_currentPage != null)
                {
                    IVisualElementRenderer renderer = Platform.GetRenderer(_currentPage);

                    if (renderer != null)
                    {
                        renderer.View?.RemoveFromParent();
                        _currentPage = null;
                    }
                }

                if (_bottomBar != null)
                {
                    _bottomBar.RemoveFromParent();
                    _bottomBar.Dispose();
                    _bottomBar = null;
                }
            }
        }

        private BottomBar CreateBottomBar(IEnumerable<Page> pageIntents)
        {
            var bar = new BottomBar(Context);

            // TODO: Configure the bottom bar here according to your needs

            bar.SetOnTabClickListener(this);
            bar.UseFixedMode();
            bar.NoTabletGoodness();
            bar.NoTopOffset();
            PopulateBottomBarItems(bar, pageIntents);

            bar.ItemContainer.SetBackgroundColor(Xamarin.Forms.Color.FromHex("#fafafa").ToAndroid());

            //bar.SetTextAppearance(
            //    Device.Idiom == TargetIdiom.Phone ? Resource.Style.TextAppearance_Small : Resource.Style.TextAppearance_Small_ForTablet
            //);
            for (int i = 0; i < bar.ItemContainer.ChildCount; i++)
            {
                var child = bar.ItemContainer.GetChildAt(i);
                child.SetPadding(0, 0, 0, 0);
            }

            return bar;
        }

        private void PopulateBottomBarItems(BottomBar bar, IEnumerable<Page> pages)
        {
            var barItems = pages.Select(x => new BottomBarTab(Context.Resources.GetDrawable(x.Icon), x.Title));
            bar.SetItems(barItems.ToArray());
        }

        private void LoadPageContent(int position)
        {
            ShowPage(position);
        }

        private void ShowPage(int position)
        {
            if (position != _lastSelectedTabIndex)
            {
                Element.CurrentPage = Element.Children[position];

                if (Element.CurrentPage != null)
                {
                    LoadPageContent(Element.CurrentPage);
                }
            }

            _lastSelectedTabIndex = position;
        }

        private void LoadPageContent(Page page)
        {
            UnloadCurrentPage();

            _currentPage = page;

            LoadCurrentPage();

            Element.CurrentPage = _currentPage;
        }

        private void LoadCurrentPage()
        {
            var renderer = Platform.GetRenderer(_currentPage);
            if (renderer == null)
            {
                renderer = Platform.CreateRendererWithContext(_currentPage, Context);
                Platform.SetRenderer(_currentPage, renderer);
                AddView(renderer.View);
            }
            else
            {
                //var basePage = _currentPage as NavigationPageGradientHeader;
                //var page = basePage?.CurrentPage as BasePage;
                //page?.SendAppearing();
            }
            renderer.View.Visibility = ViewStates.Visible;
        }

        private void UnloadCurrentPage()
        {
            if (_currentPage != null)
            {
                var renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.View.Visibility = ViewStates.Invisible;
                }
            }
        }
    }
}