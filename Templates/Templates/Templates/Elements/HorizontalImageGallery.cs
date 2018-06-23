using System.Collections;
using Xamarin.Forms;

namespace Templates.Elements
{
    /// <inheritdoc />
    /// <summary>
    /// Horizontal scroll view. Can be used as horizontal image gallery.
    /// <para>
    /// Create custom renderers for Android and iOS and call Render() method in OnElementChanged() method.
    /// </para>
    /// </summary>
    public class HorizontalImageGallery : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(HorizontalImageGallery), default(IEnumerable));

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(HorizontalImageGallery), default(DataTemplate));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// Call this method in custom renderer
        /// </summary>
        public void Render()
        {
            if (ItemTemplate == null || ItemsSource == null)
                return;

            var layout = new StackLayout
            {
                Orientation = Orientation == ScrollOrientation.Vertical
                    ? StackOrientation.Vertical
                    : StackOrientation.Horizontal
            };

            foreach (var item in ItemsSource)
            {
                var viewCell = ItemTemplate.CreateContent() as ViewCell;
                viewCell.View.BindingContext = item;
                layout.Children.Add(viewCell.View);
            }

            Content = layout;
        }
    }
}