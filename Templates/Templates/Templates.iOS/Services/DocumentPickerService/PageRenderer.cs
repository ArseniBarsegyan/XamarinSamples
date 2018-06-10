﻿using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(Templates.iOS.Services.DocumentPickerService.PageRenderer))]
namespace Templates.iOS.Services.DocumentPickerService
{
    /// <summary>
    /// Required for PlatfomDocumentPicker for iOS.
    /// </summary>
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        /// <summary>
        /// Table mapping pages to their renderers.
        /// </summary>
        internal static ConditionalWeakTable<Page, PageRenderer> Renderers = new ConditionalWeakTable<Page, PageRenderer>();

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement is Page oldPage)
            {
                Renderers.Remove(oldPage);
            }
            if (e.NewElement is Page newPage)
            {
                Renderers.Add(newPage, this);
            }
        }
    }
}