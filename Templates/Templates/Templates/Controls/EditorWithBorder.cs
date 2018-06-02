using Xamarin.Forms;

namespace Templates.Controls
{
    /// <summary>
    /// Editor with colored border.
    /// </summary>
    public class EditorWithBorder : Editor
    {
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EditorWithBorder), Color.DodgerBlue, BindingMode.Default);

        /// <summary>
        /// Border color.
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
    }
}