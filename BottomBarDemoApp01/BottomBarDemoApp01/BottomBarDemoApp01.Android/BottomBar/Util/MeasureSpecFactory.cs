using Android.Views;

namespace BottomBarDemoApp01.Droid.BottomBar.Util
{
    public static class MeasureSpecFactory
    {
        public static int GetSize(int measureSpec)
        {
            return measureSpec & 1073741823;
        }

        public static int MakeMeasureSpec(int size, MeasureSpecMode mode)
        {
            return (int)(size + mode);
        }
    }
}