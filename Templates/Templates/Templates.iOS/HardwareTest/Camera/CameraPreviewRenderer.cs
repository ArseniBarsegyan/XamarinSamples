using Templates.HardwareTest.Camera;
using Templates.iOS.HardwareTest.Camera;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraPreviewRenderer))]
namespace Templates.iOS.HardwareTest.Camera
{
    public class CameraPreviewRenderer : ViewRenderer<CameraView, UICameraPreview>
    {
        private UICameraPreview _uiCameraPreview;

        protected override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _uiCameraPreview = new UICameraPreview(e.NewElement.Camera);
                SetNativeControl(_uiCameraPreview);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.CaptureSession.Dispose();
                Control.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}