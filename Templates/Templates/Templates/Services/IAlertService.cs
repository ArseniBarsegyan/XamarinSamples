using System.Threading.Tasks;

namespace Templates.Services
{
    /// <summary>
    /// This service provides custom alerts.
    /// </summary>
    public interface IAlertService
    {
        void ShowAlert(string message);
        Task<bool> ShowResolution(string message);
        void ShowOkAlert(string message, string okButtonText);
        Task<bool> ShowUploadAlert(string message, string okButtonText);
        Task<bool> ShowYesNoAlert(string message, string yesButtonText, string noButtonText);
        void ShowWarningAlert(string message, string warningButtonText);
    }
}