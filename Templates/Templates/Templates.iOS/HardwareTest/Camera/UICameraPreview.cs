using System;
using System.Linq;
using AVFoundation;
using CoreGraphics;
using Foundation;
using Templates.HardwareTest.Camera;
using UIKit;

namespace Templates.iOS.HardwareTest.Camera
{
    public class UICameraPreview : UIView
    {
        private AVCaptureVideoPreviewLayer _previewLayer;
        private readonly CameraOptions _cameraOptions;

        public UICameraPreview(CameraOptions options)
        {
            _cameraOptions = options;
            IsPreviewing = false;
            Initialize();
        }

        public AVCaptureSession CaptureSession { get; private set; }
        public bool IsPreviewing { get; set; }

        public event EventHandler<EventArgs> Tapped;

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            _previewLayer.Frame = rect;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            OnTapped();
        }

        protected virtual void OnTapped()
        {
            var eventHandler = Tapped;
            eventHandler?.Invoke(this, new EventArgs());
        }

        private void Initialize()
        {
            CaptureSession = new AVCaptureSession();
            _previewLayer = new AVCaptureVideoPreviewLayer(CaptureSession)
            {
                Frame = Bounds,
                VideoGravity = AVLayerVideoGravity.ResizeAspectFill
            };

            var videoDevices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);
            var cameraPosition = (_cameraOptions == CameraOptions.Front) ? AVCaptureDevicePosition.Front : AVCaptureDevicePosition.Back;
            var device = videoDevices.FirstOrDefault(d => d.Position == cameraPosition);

            if (device == null)
            {
                return;
            }

            NSError error;
            var input = new AVCaptureDeviceInput(device, out error);
            CaptureSession.AddInput(input);
            Layer.AddSublayer(_previewLayer);
            CaptureSession.StartRunning();
            IsPreviewing = true;
        }
    }
}