using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.Controls.Carousel
{
    /// <summary>
    /// Provide view which contains carousel, title and description for all items in carousel.
    /// <para>
    /// Set item source and data template property to customize carousel.
    /// </para>
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TourView : ContentView
    {
        private Carousel _carousel;
        private bool _isInitialized;

        public TourView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
                typeof(IEnumerable<CarouselContent>),
                typeof(TourView),
                null);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(TourView),
                null);

        /// <summary>
        /// Invokes when user click button.
        /// </summary>
        public event EventHandler StartButtonClicked;

        /// <summary>
        /// List of <seealso cref="CarouselContent"/> as item source for carousel.
        /// </summary>
        public IEnumerable<CarouselContent> ItemsSource
        {
            get => (IEnumerable<CarouselContent>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Item template for carousel view.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                _carousel.ItemTemplate = value;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (!_isInitialized)
            {
                _carousel = new Carousel(new ObservableCollection<CarouselContent>());
                _isInitialized = true;
            }

            if (propertyName == nameof(ItemsSource))
            {
                _carousel.Pages = new ObservableCollection<CarouselContent>(ItemsSource);

                _carousel.PageChanged += ViewOnPageChanged;
                CarouselViewContainer.Children.Add(_carousel);

                TitleLabel.Text = ItemsSource.ElementAt(0).Title;
                DescriptionLabel.Text = ItemsSource.ElementAt(0).Description;
            }
            else if (propertyName == nameof(ItemTemplate))
            {
                _carousel.ItemTemplate = ItemTemplate;
            }
        }

        // When current page is the last page in carousel, button will become visible.
        private void ViewOnPageChanged(object sender, int i)
        {
            var content = _carousel.Pages;
            TitleLabel.Text = content.ElementAt(i).Title;
            DescriptionLabel.Text = content.ElementAt(i).Description;

            StartButton.IsVisible = i == _carousel.Pages.Count - 1;
        }

        private void StartButton_OnClicked(object sender, EventArgs e)
        {
            StartButtonClicked?.Invoke(this, e);
        }
    }
}