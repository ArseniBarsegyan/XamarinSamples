using System.Collections.ObjectModel;
using Templates.Controls.Carousel;

namespace Templates.ViewModels
{
    /// <summary>
    /// View model for Tour page. Provide observable collection of Carousel content.
    /// </summary>
    public class TourPageViewModel
    {
        public ObservableCollection<CarouselContent> CarouselContents { get; set; } = new ObservableCollection<CarouselContent>
        {
            new CarouselContent { Title = "Image 1", Description = "Test text 1", ImageSource = "https://html5box.com/html5lightbox/images/skynight.jpg"},
            new CarouselContent { Title = "Image 2", Description = "Test text 2", ImageSource = "http://s1.funon.cc/img/orig/201710/26/59f1d676d4e5e.jpg"},
            new CarouselContent { Title = "Image 3", Description = "Test text 3", ImageSource = "http://www.astronomy.com/~/media/FE6184E41B894F069C07E6699FB41F64.jpg"}
        };
    }
}