using System.Collections.ObjectModel;
using System.Linq;
using Templates.Elements;
using Templates.Pages;
using Xamarin.Forms;

namespace Templates.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// View model for <see cref="T:Templates.Pages.ElementsDemoPage" />.
    /// </summary>
    public class ElementsDemoViewModel : BaseViewModel
    {
        public ElementsDemoViewModel()
        {
            TestImages = new ObservableCollection<Image>
            {
                new Image { Source = "https://www.w3schools.com/w3css/img_lights.jpg" },
                new Image { Source = "https://wallpaperbrowse.com/media/images/beautiful-sunset-images-196063.jpg" },
                new Image { Source = "http://ujszo.com/sites/default/files/lead_image/f%C3%B6ld.jpg" },
                new Image { Source = "https://www.planwallpaper.com/static/images/121.jpg" },
                new Image { Source = "http://wall2born.com/data/out/209/image-43680510-wallpapers-hd-free-download-for-laptop.jpg" },
                new Image { Source = "http://www.skriveri.lv/wp-content/uploads/2018/05/green-trees.jpeg" }
            };
            SelectedImage = TestImages.ElementAt(0);
        }

        public ObservableCollection<Image> TestImages { get; set; }
        public Image SelectedImage { get; set; }
    }
}