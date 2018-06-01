using MyDiary.Pages;
using System;
using System.Collections.Generic;

namespace MyDiary.Helpers
{
    /// <summary>
    /// Helper class. Provide list of MasterPageItem.
    /// </summary>
    public static class MenuHelper
    {
        public static List<MasterPageItem> GetMenu()
        {
            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Notes",
                    IconSource = "note.png",
                    TargetType = typeof(NotesPage),
                    IsDisplayed = true
                },
            };
            return masterPageItems;
        }
    }

    public class MasterPageItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
        public bool IsDisplayed { get; set; }
    }

    public enum MenuPageIndex
    {
        NotesPage
    }
}