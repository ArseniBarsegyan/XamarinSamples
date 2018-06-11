﻿using MyDiary.Pages;
using System;
using System.Collections.Generic;

namespace MyDiary.Helpers
{
    /// <summary>
    /// Helper class. Provide list of MasterPageItem for MenuPage.
    /// </summary>
    public static class MenuHelper
    {
        public static List<MasterPageItem> GetMenu()
        {
            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = ConstantHelper.Notes,
                    IconSource = ConstantHelper.NotesListIcon,
                    TargetType = typeof(NotesPage),
                    IsDisplayed = true
                },
            };
            return masterPageItems;
        }
    }

    public class MasterPageItem
    {
        /// <summary>
        /// Title that will be displayed in side menu.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Icon source that will be displayed in side menu.
        /// </summary>
        public string IconSource { get; set; }
        /// <summary>
        /// Page on which user will be redirected.
        /// </summary>
        public Type TargetType { get; set; }
        /// <summary>
        /// Show this item in side menu or not.
        /// </summary>
        public bool IsDisplayed { get; set; }
    }

    public enum MenuPageIndex
    {
        NotesPage
    }
}