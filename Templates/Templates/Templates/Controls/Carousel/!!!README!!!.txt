.Net Standard Library:
Install Xamarin.Forms.CarouselView (prerelease)


#Android: In MainActivity.cs paste there lines after line global::Xamarin.Forms.Forms.Init(this, bundle);:

var cv = typeof(Xamarin.Forms.CarouselView);
var assembly = Assembly.Load(cv.FullName);



#iOS: In AppDelegate.cs paste there lines after line global::Xamarin.Forms.Forms.Init(this, bundle);:

var cv = typeof(Xamarin.Forms.CarouselView);
var assembly = Assembly.Load(cv.FullName);

Add TourView to any page as element, set data template and itemssource properties and handle StartButtonClicked event.