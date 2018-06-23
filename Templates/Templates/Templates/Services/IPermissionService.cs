using System.Threading.Tasks;

namespace Templates.Services
{
    /// <summary>
    /// Provide ability to ask permission from user.
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Ask permission from user.
        /// </summary>
        Task<bool> AskPermission();
    }
}