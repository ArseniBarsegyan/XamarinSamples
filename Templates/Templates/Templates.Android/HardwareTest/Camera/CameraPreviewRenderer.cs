using Android.Content;
using Templates.Droid.HardwareTest.Camera;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CameraView = Templates.HardwareTest.Camera.CameraView;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraPreviewRenderer))]
namespace Templates.Droid.HardwareTest.Camera
{
    public class CameraPreviewRenderer : ViewRenderer<CameraView, CameraPreview>
    {
        private CameraPreview _cameraPreview;

        public CameraPreviewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _cameraPreview = new CameraPreview(Context);
                SetNativeControl(_cameraPreview);
            }

            if (e.NewElement != null)
            {
                Control.Preview = Android.Hardware.Camera.Open((int)e.NewElement.Camera);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.Preview.Release();
            }
            base.Dispose(disposing);
        }
    }
}