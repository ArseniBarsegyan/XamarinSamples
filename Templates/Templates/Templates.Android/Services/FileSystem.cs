using System;
using System.IO;
using Templates.Droid.Services;
using Templates.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystem))]
namespace Templates.Droid.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IFileSystem"/> for Android.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public byte[] ReadAllBytes(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return new byte[0];
            }
        }
    }
}