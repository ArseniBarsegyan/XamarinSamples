using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MyDiary.Extensions;
using Xamarin.Forms;

namespace MyDiary.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        public NoteViewModel()
        {
            UpdateNoteCommand = new Command<NoteViewModel>(UpdateNoteCommandExecute);
            DeleteNoteCommand = new Command<NoteViewModel>(note => DeleteNoteCommandExecute(note));
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string FullDescription { get; set; }

        public ICommand UpdateNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        public ObservableCollection<PhotoViewModel> Photos { get; set; }

        private void UpdateNoteCommandExecute(NoteViewModel viewModel)
        {
            App.NoteRepository.Save(viewModel.ToNoteModel());
        }

        private int DeleteNoteCommandExecute(NoteViewModel viewModel)
        {
            return App.NoteRepository.DeleteNote(viewModel.ToNoteModel());
        }
    }
}