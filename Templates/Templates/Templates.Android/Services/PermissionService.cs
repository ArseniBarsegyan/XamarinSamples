using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Templates.Droid.Services;
using Templates.Services;

[assembly: Xamarin.Forms.Dependency(typeof(PermissionService))]
namespace Templates.Droid.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IPermissionService"/> for Android.
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private static readonly int SdkVersion = (int)Android.OS.Build.VERSION.SdkInt;

        public async Task<bool> AskPermission()
        {
            //Runtime permissions available only from API25+
            if (SdkVersion < 23)
            {
                return true;
            }

            try
            {
                //Configure required permissions here and include them into manifest
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                }
                return await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location) == PermissionStatus.Granted;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}