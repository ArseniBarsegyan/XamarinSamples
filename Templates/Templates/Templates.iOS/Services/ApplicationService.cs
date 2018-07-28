using System.Threading;
using Templates.iOS.Services;
using Templates.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ApplicationService))]
namespace Templates.iOS.Services
{
    /// <summary>
    /// Implementation of <see cref="IApplicationService"/>.
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}