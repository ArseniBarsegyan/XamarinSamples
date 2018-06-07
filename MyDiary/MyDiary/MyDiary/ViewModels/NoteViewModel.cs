using System;
using System.Collections.Generic;
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
            CreateOrUpdateNoteCommand = new Command<NoteViewModel>(CreateOrUpdateNoteCommandExecute);
            DeleteNoteCommand = new Command<NoteViewModel>(note => DeleteNoteCommandExecute(note));
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string FullDescription { get; set; }

        public ICommand CreateOrUpdateNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        public List<PhotoViewModel> Photos { get; set; }

        private void CreateOrUpdateNoteCommandExecute(NoteViewModel viewModel)
        {
            // If there is no photos in list, add photomodel with empty image
            if (!Photos.Any())
            {
                Photos.Add(new PhotoViewModel
                {
                    ResizedPath = "empty_note.jpg",
                    Thumbnail = "empty_note.jpg"
                });
            }
            App.NoteRepository.Save(viewModel.ToNoteModel());
        }

        private int DeleteNoteCommandExecute(NoteViewModel viewModel)
        {
            return App.NoteRepository.DeleteNote(viewModel.ToNoteModel());
        }
    }
}