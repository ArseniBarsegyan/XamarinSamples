using Xamarin.Forms;

namespace Templates.Controls.Carousel
{
    /// <summary>
    /// Represents horizontal container for DotImage and configure DotImage.
    /// </summary>
    public class DotButtonsLayout : StackLayout
    {
        public DotButtonsLayout(int dotCount, int dotSize)
        {
            Dots = new DotImage[dotCount];

            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.EndAndExpand;
            HorizontalOptions = LayoutOptions.Center;
            Margin = new Thickness(0, 0, 0, 20);

            for (var i = 0; i < dotCount; i++)
            {
                Dots[i] = new DotImage
                {
                    Source = "emptyDot.png",
                    HeightRequest = dotSize * 1.2,
                    WidthRequest = dotSize,
                    Index = i,
                    Layout = this
                };
                Children.Add(Dots[i]);
            }
            if (Dots.Length > 0)
            {
                Dots[0].Source = "filledDot.png";
            }
        }

        public DotImage[] Dots { get; set; }
    }
}