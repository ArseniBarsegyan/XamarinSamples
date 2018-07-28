using Plugin.CurrentActivity;
using Templates.Droid.Services;
using Templates.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ApplicationService))]
namespace Templates.Droid.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="T:Templates.Services.IApplicationService" />.
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        public void CloseApplication()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            activity.Finish();
        }
    }
}