using Android.Content;
using Android.Text.Method;
using Android.Views;
using MyDiary.Droid.Renderers;
using MyDiary.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScrollableEditor), typeof(ScrollableEditorRenderer))]
namespace MyDiary.Droid.Renderers
{
    public class ScrollableEditorRenderer : EditorRenderer
    {
        public ScrollableEditorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = (global::Android.Widget.EditText)Control;

                // While scrolling inside Editor stop scrolling parent view.
                nativeEditText.OverScrollMode = OverScrollMode.Always;
                nativeEditText.ScrollBarStyle = ScrollbarStyles.InsideInset;
                nativeEditText.SetOnTouchListener(new DroidTouchListener());

                // For Scrolling in Editor inner area
                Control.VerticalScrollBarEnabled = true;
                Control.MovementMethod = ScrollingMovementMethod.Instance;
                Control.ScrollBarStyle = ScrollbarStyles.InsideInset;
                // Remove underline
                Control.Background = null;
                // Force scrollbars to be displayed
                Android.Content.Res.TypedArray a = Control.Context.Theme.ObtainStyledAttributes(new int[0]);
                InitializeScrollbars(a);
                a.Recycle();
            }
        }
    }

    internal class DroidTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            v.Parent?.RequestDisallowInterceptTouchEvent(true);
            if ((e.Action & MotionEventActions.Up) != 0 && (e.ActionMasked & MotionEventActions.Up) != 0)
            {
                v.Parent?.RequestDisallowInterceptTouchEvent(false);
            }
            return false;
        }
    }
}