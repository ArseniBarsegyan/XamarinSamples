using Foundation;
using Templates.Controls;
using Templates.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPickerWithIcon), typeof(CustomPickerWithIconRenderer))]
namespace Templates.iOS.Renderers
{
    /// <summary>
    /// Renderer for <see cref="CustomPickerWithIcon" />
    /// </summary>
    public class CustomPickerWithIconRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = Element as CustomPickerWithIcon;

            if (Control != null && Element != null && !string.IsNullOrEmpty(element?.Image))
            {
                var arrow = UIImage.FromBundle(element.Image);
                Control.RightViewMode = UITextFieldViewMode.Always;

                UIColor color = element.PlaceholderColor.ToUIColor();
                if (element.Title != null)
                {
                    var placeholderAttributes = new NSAttributedString(element.Title, new UIStringAttributes { ForegroundColor = color });
                    Control.AttributedPlaceholder = placeholderAttributes;
                }

                var image = new UIImageView(arrow);
                image.ContentMode = UIViewContentMode.Center;
                Control.RightView = image;
                Control.RightView.Frame = new CoreGraphics.CGRect(0, 0, 50, Bounds.Height);

                Control.Layer.BorderColor = element.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = 1;
                Control.ClipsToBounds = true;

                Layer.CornerRadius = 0.0f;
                Layer.MasksToBounds = true;
            }
        }

        // These methods can be useful, if make placeholder color as string value.

        private float GetRed(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.R;
        }

        private float GetGreen(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.G;
        }

        private float GetBlue(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.B;
        }
    }
}