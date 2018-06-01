﻿using MyDiary.Interfaces;
using MyDiary.Models;
using MyDiary.Pages;
using Xamarin.Forms;

namespace MyDiary
{
	public partial class App : Application
	{
	    private static NoteRepository _noteRepository;

        public App ()
		{
			InitializeComponent();
		    MainPage = new NavigationPage(new MenuPage());
        }

	    /// <summary>
	    /// Gets the Note repository through dependency service.
	    /// </summary>
	    /// <value>The database.</value>
	    public static NoteRepository NoteRepository => _noteRepository ??
	                                             (_noteRepository = new NoteRepository(DependencyService.Get<IFileHelper>()
	                                                 .GetLocalFilePath("MyDiaryDB.db3")));

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
