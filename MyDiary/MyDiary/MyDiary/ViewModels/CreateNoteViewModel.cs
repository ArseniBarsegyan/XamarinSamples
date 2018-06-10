﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            CreateNoteCommand = new Command<NoteViewModel>(CreateNoteCommandExecute);
            DeleteNoteCommand = new Command<NoteViewModel>(note => DeleteNoteCommandExecute(note));
        }

        public ObservableCollection<PhotoViewModel> Photos { get; set; }

        /// <summary>
        /// Invokes when photo added to Photos.
        /// </summary>
        public event EventHandler PhotoAdded;

        public ICommand TakePhotoCommand { get; set; }
        public ICommand CreateNoteCommand { get; set; }
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

        private void CreateNoteCommandExecute(NoteViewModel viewModel)
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