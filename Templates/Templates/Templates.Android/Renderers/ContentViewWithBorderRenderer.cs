using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Templates.Controls;
using Templates.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentViewWithBorder), typeof(ContentViewWithBorderRenderer))]
namespace Templates.Droid.Renderers
{
    /// <summary>
    /// Renderer for <see cref="ContentViewWithBorder"/>
    /// </summary>
    public class ContentViewWithBorderRenderer : VisualElementRenderer<ContentView>
    {
        public ContentViewWithBorderRenderer(Context context) : base(context)
        {
        }

        private ShapeDrawable _borderShape;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                return;
            }

            if (e.NewElement != null)
            {
                var element = e.NewElement as ContentViewWithBorder;
                _borderShape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RoundRectShape(new float[] { (float)element.CornerRadius,
                                                                                                                    (float)element.CornerRadius,
                                                                                                                    (float)element.CornerRadius,
                                                                                                                    (float)element.CornerRadius,
                                                                                                                    (float)element.CornerRadius,
                                                                                                                    (float)element.CornerRadius,
                                                                                                                    (float)element.CornerRadius,
                                                                                                                    (float)element.CornerRadius }, null, null));
                var paint = new Paint(PaintFlags.AntiAlias)
                {
                    Color = element.BorderColor.ToAndroid(),
                    StrokeWidth = (float)element.BorderWidth * 2.5f,
                    StrokeMiter = 10f
                };

                _borderShape.Paint.Set(paint);
                _borderShape.Paint.SetStyle(Paint.Style.Stroke);

                Background = _borderShape;
            }
        }
    }
}