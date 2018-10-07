﻿using System;
using System.Drawing;
using Foundation;
using Templates.iOS.Services;
using Templates.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace Templates.iOS.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IMediaService" /> for iOS.
    /// </summary>
    public class MediaService : IMediaService
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            var data = NSData.FromArray(imageData);
            UIImage originalImage = UIImage.LoadFromData(data);

            var originalHeight = originalImage.Size.Height;
            var originalWidth = originalImage.Size.Width;

            nfloat newHeight = 0;
            nfloat newWidth = 0;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                nfloat ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                nfloat ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            width = (float)newWidth;
            height = (float)newHeight;

            UIGraphics.BeginImageContext(new SizeF(width, height));
            originalImage.Draw(new RectangleF(0, 0, width, height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            var bytesImagen = resizedImage.AsJPEG().ToArray();
            resizedImage.Dispose();
            return bytesImagen;
        }
    }
}