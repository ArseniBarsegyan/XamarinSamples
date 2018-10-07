namespace Templates.Services
{
    /// <summary>
    /// Provide ability to resize images.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Resize provided image to passed width and height.
        /// </summary>
        /// <param name="imageData">Content of the image to be resized</param>
        /// <param name="width">Resized image width</param>
        /// <param name="height">Resized image height</param>
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}