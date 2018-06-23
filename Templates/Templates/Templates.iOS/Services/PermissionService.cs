using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Templates.iOS.Services;
using Templates.Services;

[assembly: Xamarin.Forms.Dependency(typeof(PermissionService))]
namespace Templates.iOS.Services
{ 
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IPermissionService"/> for iOS.
    /// </summary>
    public class PermissionService : IPermissionService
    {
        public async Task<bool> AskPermission()
        {
            try
            {
                //Configure required permissions here and include them into info.plist
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