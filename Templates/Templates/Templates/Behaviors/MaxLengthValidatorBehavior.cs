using Xamarin.Forms;

namespace Templates.Behaviors
{
    /// <inheritdoc />
    /// <summary>
    /// Checks maximum length of the Entry's text.
    /// </summary>
    public class MaxLengthValidatorBehavior : Behavior<Entry>
    {
        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(MaxLengthValidatorBehavior), false);
        private static readonly BindableProperty ValidNumberTextColorProperty = BindableProperty.Create(nameof(ValidNumberTextColor), typeof(Color), typeof(MaxLengthValidatorBehavior), Color.Black);
        private static readonly BindableProperty InvalidNumberTextColorProperty = BindableProperty.Create(nameof(InvalidNumberTextColor), typeof(Color), typeof(MaxLengthValidatorBehavior), Color.Red);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaxLengthValidatorBehavior), 1);

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        public Color ValidNumberTextColor
        {
            get => (Color)GetValue(ValidNumberTextColorProperty);
            set => SetValue(ValidNumberTextColorProperty, value);
        }

        public Color InvalidNumberTextColor
        {
            get => (Color)GetValue(InvalidNumberTextColorProperty);
            set => SetValue(InvalidNumberTextColorProperty, value);
        }

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length < MaxLength)
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
            ((Entry)sender).TextColor = IsValid ? ValidNumberTextColor : InvalidNumberTextColor;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
        }
    }
}