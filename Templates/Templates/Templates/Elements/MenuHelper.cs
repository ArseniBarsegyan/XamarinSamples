using System;
using System.Collections.Generic;
using Templates.HardwareTest.Camera;
using Templates.HardwareTest.Microphone;
using Templates.HardwareTest.Sound;
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
                    Title = "Tour demo page",
                    IconSource = "",
                    TargetType = typeof(TourPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Controls demo page",
                    IconSource = "",
                    TargetType = typeof(ControlsDemoPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Elements demo page",
                    IconSource = "",
                    TargetType = typeof(ElementsDemoPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Alerts demo page",
                    IconSource = "",
                    TargetType = typeof(AlertsDemoPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Upload document demo page",
                    IconSource = "",
                    TargetType = typeof(UploadDocumentDemoPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Camera preview page",
                    IconSource = "",
                    TargetType = typeof(CameraPreviewPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Microphone testing page",
                    IconSource = "",
                    TargetType = typeof(MicrophoneTestingPage),
                    IsDisplayed = true
                },
                new MasterPageItem
                {
                    Title = "Sound testing page",
                    IconSource = "",
                    TargetType = typeof(SoundTestingPage),
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
        ElementsPage,
        AlertsPage,
        TourPage,
        UploadPage,
        CameraTestPage,
        MicrophoneTestPage,
        SoundTestPage
    }
}