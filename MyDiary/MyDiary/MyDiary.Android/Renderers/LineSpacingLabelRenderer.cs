using Android.Content;
using Android.Text;
using MyDiary.Droid.Renderers;
using MyDiary.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineSpacingLabel), typeof(LineSpacingLabelRenderer))]
namespace MyDiary.Droid.Renderers
{
    public class LineSpacingLabelRenderer : LabelRenderer
    {
        protected LineSpacingLabel LineSpacingLabel { get; private set; }

        public LineSpacingLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                LineSpacingLabel = Element as LineSpacingLabel;
            }

            if (Control != null)
            {
                var lineSpacing = LineSpacingLabel.LineSpacing;
                Control.SetLineSpacing(1f, (float)lineSpacing);
                Control.SetSingleLine(false);
                Control.SetMaxLines(LineSpacingLabel.Lines);
                UpdateLayout();
            }
        }
    }
}