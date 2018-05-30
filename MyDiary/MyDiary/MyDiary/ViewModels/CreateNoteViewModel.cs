using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MyDiary.Extensions;
using MyDiary.Helpers;
using Xamarin.Forms;

namespace MyDiary.ViewModels
{
    public class CreateNoteViewModel : BaseViewModel
    {
        private readonly MediaHelper _mediaHelper;

        public CreateNoteViewModel()
        {
            _mediaHelper = new MediaHelper();
            Photos = new ObservableCollection<PhotoViewModel>();

            TakePhotoCommand = new Command(async () => await TakePhotoCommandExecute());
            CreateOrUpdateNoteCommand = new Command<NoteViewModel>(CreateOrUpdateNoteCommandExecute);
            DeleteNoteCommand = new Command<NoteViewModel>(note => DeleteNoteCommandExecute(note));
        }

        public ObservableCollection<PhotoViewModel> Photos { get; set; }

        /// <summary>
        /// Invokes when photo added to Photos.
        /// </summary>
        public event EventHandler PhotoAdded;

        public ICommand TakePhotoCommand { get; set; }
        public ICommand CreateOrUpdateNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        private async Task TakePhotoCommandExecute()
        {
            var photoModel = await _mediaHelper.TakePhotoAsync();
            if (photoModel != null)
            {
                Photos.Add(photoModel.ToPhotoViewModel());
                PhotoAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        private void CreateOrUpdateNoteCommandExecute(NoteViewModel viewModel)
        {
            App.NoteRepository.Save(viewModel.ToNoteModel());
        }

        private int DeleteNoteCommandExecute(NoteViewModel viewModel)
        {
            return App.NoteRepository.DeleteNote(viewModel.ToNoteModel());
        }
    }
}