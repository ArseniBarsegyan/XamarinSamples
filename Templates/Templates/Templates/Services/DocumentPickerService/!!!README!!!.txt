Implement IPlatformDocumentPicker on Android and iOS.

#Android:
Required CrossCurrentActivity plugin setup in your project.
Add Platform.cs class to your project.
Add following lines to MainActivity.cs:

add this field: 
	public event Action<int, Result, Intent> ActivityResult;

add following line after global::Xamarin.Forms.Forms.Init(this, bundle):
	Services.Platform.Init(this);

add this method:

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        ActivityResult?.Invoke(requestCode, resultCode, data);
    }

#iOS:
Copy page extension and PageRenderer in your project.