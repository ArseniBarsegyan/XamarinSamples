using Xamarin.Forms;

namespace Templates.Controls.Carousel
{
    /// <summary>
    /// Reprensents one page for carousel.
    /// </summary>
    public class CarouselContent
    {
        public int Position { get; set; }
        public string ImageSource { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Color BackgroundColor { get; set; }
    }
}