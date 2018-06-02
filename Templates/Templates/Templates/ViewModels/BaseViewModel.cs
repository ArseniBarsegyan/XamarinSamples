using System.ComponentModel;

namespace Templates.ViewModels
{
    /// <summary>
    /// Extend this class when create new view model.
    /// <para>
    /// Also required NuGET package PropertyChanged.Fody with FodyWeavers.xml file in .Net Standard library.
    /// </para>
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}