﻿using Xamarin.Forms;

namespace Templates.HardwareTest.Camera
{
    public class CameraView : View
    {
        public static readonly BindableProperty CameraProperty = BindableProperty.Create(nameof(Camera), typeof(CameraOptions), typeof(CameraView), CameraOptions.Rear);

        public CameraOptions Camera
        {
            get => (CameraOptions)GetValue(CameraProperty);
            set => SetValue(CameraProperty, value);
        }
    }
}