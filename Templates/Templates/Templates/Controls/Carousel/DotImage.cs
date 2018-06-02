using Xamarin.Forms;

namespace Templates.Controls.Carousel
{
    /// <summary>
    /// Represents dot image on carousel.
    /// </summary>
    public class DotImage : Image
    {
        public DotImage()
        {
            var clickCheck = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Clicked?.Invoke(this);
                })
            };
            GestureRecognizers.Add(clickCheck);
        }

        public int Index { get; set; }
        public new DotButtonsLayout Layout { get; set; }

        public delegate void ClickHandler(DotImage sender);
        public event ClickHandler Clicked;
    }
}