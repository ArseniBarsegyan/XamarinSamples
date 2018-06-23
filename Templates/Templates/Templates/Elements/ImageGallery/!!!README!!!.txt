.Net Standard Library:
Install Xamarin.Forms.CarouselView (prerelease)

#Android: In MainActivity.cs paste there lines after line global::Xamarin.Forms.Forms.Init(this, bundle);:

var cv = typeof(Xamarin.Forms.CarouselView);
var assembly = Assembly.Load(cv.FullName);


#iOS: In AppDelegate.cs paste there lines after line global::Xamarin.Forms.Forms.Init(this, bundle);:

var cv = typeof(Xamarin.Forms.CarouselView);
var assembly = Assembly.Load(cv.FullName);

Create FullSizeImageGallery instance, pass collection of images and item you want to display first.
(The best way is to create HorizontalImageGallery with collection of images and attach tap gesture recognizer to
that image. When handling tap on image, simply pass it as item you want to display in FullSizeImageGallery)