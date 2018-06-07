﻿using System;
using System.Linq;
using MyDiary.Helpers;
using MyDiary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDiary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetailPage : ContentPage
    {
        private readonly NoteViewModel _noteViewModel;

        public NoteDetailPage(NoteViewModel noteViewModel)
        {
            InitializeComponent();
            _noteViewModel = noteViewModel;
            Title = $"{noteViewModel.Date:d}";
            DescriptionEditor.Text = noteViewModel.Description;
        }

        private async void Delete_OnClicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert
                (ConstantHelper.Warning, ConstantHelper.NoteDeleteMessage, ConstantHelper.Ok, ConstantHelper.Cancel);
            if (result)
            {
                _noteViewModel.DeleteNoteCommand.Execute(_noteViewModel);
                await Navigation.PopAsync();
            }
        }

        private void Confirm_OnClicked(object sender, EventArgs e)
        {
            _noteViewModel.Description = DescriptionEditor.Text;
            _noteViewModel.CreateOrUpdateNoteCommand.Execute(_noteViewModel);
        }
    }
}