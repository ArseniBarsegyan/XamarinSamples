using System.IO;
using Templates.iOS.Services;
using Xamarin.Forms;
using Templates.Services;

[assembly: Dependency(typeof(FileSystem))]
namespace Templates.iOS.Services
{
    /// <summary>
    /// Implementation of <see cref="IFileSystem"/> for iOS.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}