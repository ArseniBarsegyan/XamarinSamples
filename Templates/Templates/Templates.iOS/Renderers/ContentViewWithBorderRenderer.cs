using System;
using Templates.Controls;
using Templates.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentViewWithBorder), typeof(ContentViewWithBorderRenderer))]
namespace Templates.iOS.Renderers
{
    /// <summary>
    /// Renderer for <see cref="ContentViewWithBorder"/>
    /// </summary>
    public class ContentViewWithBorderRenderer : VisualElementRenderer<ContentViewWithBorder>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ContentViewWithBorder> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                return;

            Layer.CornerRadius = (float)Element.CornerRadius;
            Layer.BorderColor = Element.BorderColor.ToCGColor();
            Layer.BorderWidth = (nfloat)Element.BorderWidth;
            Layer.MasksToBounds = true;
        }
    }
}