using System;
using System.Collections.Generic;
using Templates.Pages;

namespace Templates.Elements
{
    /// <summary>
    /// Helper class. Provide list of items for MenuPage.
    /// </summary>
    public static class MenuHelper
    {
        public static List<MasterPageItem> GetMenu()
        {
            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Controls demo page",
                    IconSource = "arrow_forwand.png",
                    TargetType = typeof(ControlsDemoPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Alerts demo page",
                    IconSource = "InfoIcon.png",
                    TargetType = typeof(AlertsDemoPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Alerts demo page",
                    IconSource = "UploadDocumentIcon.png",
                    TargetType = typeof(UploadDocumentDemoPage),
                    IsDisplayed = true
                }
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
        ControlsPage,
        AlertsPage,
        TourPage,
        UploadPage
    }
}