using System;
using System.Collections.Generic;
using System.Linq;
using MyDiary.Models;
using MyDiary.ViewModels;

namespace MyDiary.Extensions
{
    public static class ModelsConverterExtension
    {
        public static PhotoViewModel ToPhotoViewModel(this PhotoModel model)
        {
            var viewModel = new PhotoViewModel
            {
                Id = model.Id,
                NoteId = model.NoteId,
                Landscape = model.Landscape,
                ResizedPath = model.ResizedPath,
                Thumbnail = model.Thumbnail
            };
            return viewModel;
        }

        public static PhotoModel ToPhotoModel(this PhotoViewModel viewModel)
        {
            var model = new PhotoModel
            {
                Id = viewModel.Id,
                NoteId = viewModel.NoteId,
                Landscape = viewModel.Landscape,
                ResizedPath = viewModel.ResizedPath,
                Thumbnail = viewModel.Thumbnail
            };
            return model;
        }

        public static IEnumerable<PhotoViewModel> ToPhotoViewModels(this IEnumerable<PhotoModel> models)
        {
            return models.Select(model => new PhotoViewModel
                {
                    Id = model.Id,
                    Landscape = model.Landscape,
                    ResizedPath = model.ResizedPath,
                    Thumbnail = model.Thumbnail,
                    NoteId = model.NoteId
                });
        }

        public static IEnumerable<PhotoModel> ToPhotoModels(this IEnumerable<PhotoViewModel> viewModels)
        {
            return viewModels.Select(viewModel => new PhotoModel
            {
                Id = viewModel.Id,
                Landscape = viewModel.Landscape,
                ResizedPath = viewModel.ResizedPath,
                Thumbnail = viewModel.Thumbnail,
                NoteId = viewModel.NoteId
            });
        }

        public static NoteViewModel ToNoteViewModel(this Note note)
        {
            var viewModel = new NoteViewModel
            {
                Id = note.Id,
                Date = note.Date,
                Description = note.Description,
                FullDescription = note.Date.ToString("dd.MM.yy") + " "+ note.Description,
                Photos = note.Photos.ToPhotoViewModels().ToList()
            };
            return viewModel;
        }

        public static Note ToNoteModel(this NoteViewModel note)
        {
            var model = new Note
            {
                Id = note.Id,
                Date = note.Date,
                Description = note.Description,
                Photos = note.Photos.ToPhotoModels().ToList()
            };
            return model;
        }

        public static IEnumerable<NoteViewModel> ToNoteViewModels(this IEnumerable<Note> models)
        {
            return models.Select(model => new NoteViewModel
                {
                    Id = model.Id,
                    Date = model.Date,
                    Description = model.Description,
                    FullDescription = model.Date.ToString("dd.MM.yy") + " " + model.Description,
                    Photos = model.Photos.ToPhotoViewModels().ToList()
                })
                .ToList();
        }

        public static IEnumerable<Note> ToNoteViewModels(this IEnumerable<NoteViewModel> viewModels)
        {
            return viewModels.Select(viewModel => new Note
                {
                    Id = viewModel.Id,
                    Date = viewModel.Date,
                    Description = viewModel.Description,
                    Photos = viewModel.Photos.ToPhotoModels().ToList()
                })
                .ToList();
        }
    }
}