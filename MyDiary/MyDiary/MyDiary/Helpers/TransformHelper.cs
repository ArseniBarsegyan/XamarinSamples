﻿using System.IO;
using System.Threading.Tasks;
using ExifLib;
using MyDiary.Models;
using PCLStorage;

namespace MyDiary.Helpers
{
    /// <summary>
    /// Provide functionality to resizing images.
    /// </summary>
    public class TransformHelper
    {
        private bool _landscape;

        /// <summary>
        /// Resizes the image at the provided filePath and stores the resized image and the thumbnail in the provided
        /// PhotoModels ResizedPath and Thumbnail, also sets the Landscape mode.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="filePath">File path.</param>
        /// <param name="photoModel">Photomodel.</param>
        public async Task ResizeAsync(string filePath, PhotoModel photoModel)
        {
            _landscape = false;
            var str = await ResizeAsync(filePath);
            photoModel.ResizedPath = str[0];
            photoModel.Thumbnail = str[1];
            photoModel.Landscape = _landscape;
        }

        /// <summary>
        /// Resizes the image at the provided filePath and returns a string array containing
        /// the Resized path at position 0 and the Thumbnail path at position 1;
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="filePath">File path.</param>
        private async Task<string[]> ResizeAsync(string filePath)
        {
            var str = new string[2];

            string folder = Path.GetDirectoryName(filePath) + "/";
            string img = folder + "R" + Path.GetFileName(filePath);
            string thumb = folder + "T" + Path.GetFileName(filePath);

            IFile vf = await FileSystem.Current.GetFileFromPathAsync(filePath);
            Stream stream2 = await vf.OpenAsync(PCLStorage.FileAccess.Read);
            JpegInfo exif = ExifReader.ReadJpeg(stream2);

            int width = 0;
            int height = 0;
            int tw = 0;
            int th = 0;

            if (exif.Width > 0)
            {
                width = exif.Width;
                height = exif.Height;
                if (width > height)
                {
                    var temp = width;
                    width = height;
                    height = temp;

                    _landscape = true;
                }
            }
            else
            {
                width = 1000;
                height = 2000;
            }
            if (exif.Width / 7 < 100)
            {
                tw = 70;
                th = 100;
            }
            else
            {
                tw = width / 7;
                th = height / 13;
            }

            ImageServiceHelper.ResizeImage(filePath, img, width, height);
            ImageServiceHelper.ResizeImage(filePath, thumb, tw, th);

            str[0] = img;
            str[1] = thumb;
            return str;
        }
    }
}