using Xamarin.Forms;

namespace Templates.Controls
{
    /// <summary>
    /// Label with additional properties.
    /// </summary>
    public class ExtendedLabel : Label
    {
        public static readonly BindableProperty IsUnderlinedProperty =
            BindableProperty.Create(nameof(IsUnderlined), typeof(bool), typeof(ExtendedLabel), true, BindingMode.Default);
        
        public static readonly BindableProperty LinesProperty =
            BindableProperty.Create(nameof(Lines), typeof(int), typeof(ExtendedLabel), 3);

        public static readonly BindableProperty LineSpacingProperty =
            BindableProperty.Create(nameof(LineSpacing), typeof(double), typeof(ExtendedLabel), 1.3);

        /// <summary>
        /// Display underline.
        /// </summary>
        public bool IsUnderlined
        {
            get => (bool)GetValue(IsUnderlinedProperty);
            set => SetValue(IsUnderlinedProperty, value);
        }

        /// <summary>
        /// How much lines will be displayed.
        /// </summary>
        public int Lines
        {
            get => (int)GetValue(LinesProperty);
            set => SetValue(LinesProperty, value);
        }

        /// <summary>
        /// Spacing between lines.
        /// </summary>
        public double LineSpacing
        {
            get => (double)GetValue(LineSpacingProperty);
            set => SetValue(LineSpacingProperty, value);
        }
    }
}