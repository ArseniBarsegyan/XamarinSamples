using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Templates.Controls;
using Templates.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EditorWithBorder), typeof(EditorWithBorderRenderer))]
namespace Templates.Droid.Renderers
{
    /// <summary>
    /// Renderer for <see cref="Templates.Controls.EditorWithBorder"/>
    /// </summary>
    public class EditorWithBorderRenderer : EditorRenderer
    {
        public EditorWithBorderRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && Element != null && Control != null)
            {
                var view = Element as EditorWithBorder;

                var gradientBackground = new GradientDrawable();
                gradientBackground.SetShape(ShapeType.Rectangle);
                gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                // Thickness of the stroke line
                gradientBackground.SetStroke(1, view.BorderColor.ToAndroid());

                Control.SetBackground(gradientBackground);

                Control.SetPadding((int)DpToPixels(Context, Convert.ToSingle(12)), Control.PaddingTop,
                    (int)DpToPixels(Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}