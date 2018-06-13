using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;

namespace Templates.Droid.HardwareTest.Camera
{
    public sealed class CameraPreview : ViewGroup, ISurfaceHolderCallback
    {
        private readonly SurfaceView _surfaceView;
        private readonly ISurfaceHolder _holder;
        private Android.Hardware.Camera.Size _previewSize;
        private IList<Android.Hardware.Camera.Size> _supportedPreviewSizes;
        private Android.Hardware.Camera _camera;
        private readonly IWindowManager _windowManager;

        public bool IsPreviewing { get; set; }

        public Android.Hardware.Camera Preview
        {
            get => _camera;
            set
            {
                _camera = value;
                if (_camera != null)
                {
                    _supportedPreviewSizes = Preview.GetParameters().SupportedPreviewSizes;
                    RequestLayout();
                }
            }
        }

        public CameraPreview(Context context)
            : base(context)
        {
            _surfaceView = new SurfaceView(context);
            AddView(_surfaceView);

            _windowManager = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            IsPreviewing = false;
            _holder = _surfaceView.Holder;
            _holder.AddCallback(this);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int width = ResolveSize(SuggestedMinimumWidth, widthMeasureSpec);
            int height = ResolveSize(SuggestedMinimumHeight, heightMeasureSpec);
            SetMeasuredDimension(width, height);

            if (_supportedPreviewSizes != null)
            {
                _previewSize = GetOptimalPreviewSize(_supportedPreviewSizes, width, height);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            _surfaceView.Measure(msw, msh);
            _surfaceView.Layout(0, 0, r - l, b - t);
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            try
            {
                Preview?.SetPreviewDisplay(holder);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Preview?.StopPreview();
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
        {
            var parameters = Preview.GetParameters();
            parameters.SetPreviewSize(_previewSize.Width, _previewSize.Height);
            RequestLayout();

            switch (_windowManager.DefaultDisplay.Rotation)
            {
                case SurfaceOrientation.Rotation0:
                    _camera.SetDisplayOrientation(90);
                    break;
                case SurfaceOrientation.Rotation90:
                    _camera.SetDisplayOrientation(0);
                    break;
                case SurfaceOrientation.Rotation270:
                    _camera.SetDisplayOrientation(180);
                    break;
            }

            Preview.SetParameters(parameters);
            Preview.StartPreview();
            IsPreviewing = true;
        }

        Android.Hardware.Camera.Size GetOptimalPreviewSize(IList<Android.Hardware.Camera.Size> sizes, int w, int h)
        {
            double ASPECT_TOLERANCE = 0.1;
            double targetRatio = (double)h / w;

            if (sizes == null)
            {
                return null;
            }

            Android.Hardware.Camera.Size optimalSize = null;
            double minDiff = Double.MaxValue;

            int targetHeight = h;

            foreach (Android.Hardware.Camera.Size size in sizes)
            {
                double ratio = (double)size.Width / size.Height;

                if (Math.Abs(ratio - targetRatio) > ASPECT_TOLERANCE)
                {
                    continue;
                }

                if (Math.Abs(size.Height - targetHeight) < minDiff)
                {
                    optimalSize = size;
                    minDiff = Math.Abs(size.Height - targetHeight);
                }
            }

            if (optimalSize == null)
            {
                minDiff = Double.MaxValue;
                foreach (Android.Hardware.Camera.Size size in sizes)
                {
                    if (Math.Abs(size.Height - targetHeight) < minDiff)
                    {
                        optimalSize = size;
                        minDiff = Math.Abs(size.Height - targetHeight);
                    }
                }
            }

            return optimalSize;
        }
    }
}