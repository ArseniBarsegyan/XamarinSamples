using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MyDiary.Extensions;
using Xamarin.Forms;

namespace MyDiary.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Notes = new ObservableCollection<NoteViewModel>();
            RefreshListCommand = new Command(RefreshCommandExecute);
            SelectNoteCommand = new Command<int>(id => SelectNoteCommandExecute(id));
        }

        public bool IsRefreshing { get; set; }
        public ObservableCollection<NoteViewModel> Notes { get; set; }
        public ICommand RefreshListCommand { get; set; }
        public ICommand SelectNoteCommand { get; set; }

        public void OnAppearing()
        {
            LoadNoteFromDatabase();
        }

        private void RefreshCommandExecute()
        {
            IsRefreshing = true;
            LoadNoteFromDatabase();
            IsRefreshing = false;
        }

        private void LoadNoteFromDatabase()
        {
            var noteModels = App.NoteRepository.GetAll();
            Notes = new ObservableCollection<NoteViewModel>(noteModels.ToNoteViewModels().Reverse());
        }

        private NoteViewModel SelectNoteCommandExecute(int id)
        {
            return App.NoteRepository.GetNoteAsync(id).ToNoteViewModel();
        }
    }
}