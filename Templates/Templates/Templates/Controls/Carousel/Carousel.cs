using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Templates.Controls.Carousel
{
    public class Carousel : AbsoluteLayout
    {
        private DotButtonsLayout _dotLayout;
        private readonly CarouselView _carousel;

        public Carousel(ObservableCollection<CarouselContent> pages)
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;

            _carousel = new CarouselView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HeightRequest = 470
            };

            Pages = pages;
            _carousel.ItemSource = pages;
            _carousel.PositionSelected += OnPageChanged;

            Children.Add(_carousel);
            SetLayoutBounds(_carousel, new Rectangle(0, 0, 1, 1));
            SetLayoutFlags(_carousel, AbsoluteLayoutFlags.All);

            CreateDotLayout();
        }

        // Create layout with clickable dots. You can customize position of dots here.
        private void CreateDotLayout()
        {
            // Create the button layout with as many buttons as there are pages.
            _dotLayout = new DotButtonsLayout(Pages.Count, 10);

            foreach (var dot in _dotLayout.Dots)
            {
                dot.Clicked += DotOnClicked;
            }

            Children.Add(_dotLayout);
            SetLayoutBounds(_dotLayout, new Rectangle(0, 1, 1, 1));
            SetLayoutFlags(_dotLayout, AbsoluteLayoutFlags.All);
        }

        /// <summary>
        /// Collection of <seealso cref="CarouselContent"/> as item source for carousel.
        /// </summary>
        public ObservableCollection<CarouselContent> Pages
        {
            get => (ObservableCollection<CarouselContent>)_carousel.ItemsSource;
            set
            {
                // When pages collection is changing, unsubscribe of all dot layout events and create dot layout again.
                _carousel.ItemsSource = value;
                if (_dotLayout != null)
                {
                    foreach (var dot in _dotLayout.Dots)
                    {
                        dot.Clicked -= DotOnClicked;
                    }
                    Children.Remove(_dotLayout);
                    CreateDotLayout();
                }
            }
        }

        /// <summary>
        /// Item template for carousel.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => _carousel.ItemTemplate;
            set => _carousel.ItemTemplate = value;
        }

        public event EventHandler<int> PageChanged;

        private void OnPageChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            // Get the selected page
            var position = (int)e.SelectedPosition;
            // Set selected button color to blue, all other buttons color to white
            for (var i = 0; i < _dotLayout.Dots.Length; i++)
            {
                _dotLayout.Dots[i].Source = position == i ? "filledDot.png" : "emptyDot.png";
            }
            PageChanged?.Invoke(this, position);
        }

        private void DotOnClicked(DotImage sender)
        {
            // Get the selected buttons index
            var index = sender.Index;
            // Set the corresponding page as position of the carousel view
            _carousel.Position = index;
        }
    }
}