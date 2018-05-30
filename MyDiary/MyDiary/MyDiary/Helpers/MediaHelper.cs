using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PCLStorage;
using Plugin.Media;
using PhotoModel = MyDiary.Models.PhotoModel;

namespace MyDiary.Helpers
{
    /// <summary>
    /// Class taking photos with Plugin.Media Nuget and stores them in ObservableCollection of <see cref="T:ConvertyFire1.Models.PhotoModel"/>
    /// Resizing these photos with <see cref="T:MyDiary.Helpers.TransformHelper"/> class.
    /// Can also be used to delete files from filepaths or entire PhotoModels.
    /// </summary>
    public class MediaHelper
    {
        private readonly TransformHelper _transformHelper;

        public MediaHelper()
        {
            _transformHelper = new TransformHelper();
        }

        /// <summary>
        /// Launches the camera using Plugin.Media and lets the user take one photo.
        /// Then resizes this photo and adds avaliable exif information.
        /// Finally the everything gets stored in a PhotoModel and added to the ObservableCollection.
        /// </summary>
        public async Task<PhotoModel> TakePhotoAsync()
        {
            bool b = await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            var dt = DateTime.Now;
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                Directory = "Photos",
                Name = $"{dt:yyyyMMdd}_{dt:HHmmss}.jpg",
                SaveToAlbum = true
            });

            if (file != null)
            {
                var pm = new PhotoModel();

                await _transformHelper.ResizeAsync(file.Path, pm);
                await DeleteFileAsync(file.Path);

                return pm;
            }
            return null;
        }

        /// <summary>
        /// Deletes the PhotoModel and its pictures using PCLStorage.
        /// </summary>
        /// <param name="pm">The PhotoModel to be deleted</param>
        public async Task DeletePhotoModelAsync(PhotoModel pm)
        {
            await DeleteFileAsync(pm.ResizedPath);
            await DeleteFileAsync(pm.Thumbnail);
            // await App.PhotosRepository.DeletePhotoAsync(pm);
        }

        /// <summary>
        /// Deletes the file at the provided filepath using PCLStorage.
        /// </summary>
        /// <param name="filepath">The filepath to the file for deletion.</param>
        public async Task DeleteFileAsync(string filepath)
        {
            var file = await FileSystem.Current.GetFileFromPathAsync(filepath);
            try
            {
                await file.DeleteAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}