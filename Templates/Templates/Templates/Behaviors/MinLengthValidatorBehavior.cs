using Xamarin.Forms;

namespace Templates.Behaviors
{
    /// <inheritdoc />
    /// <summary>
    /// Checks minimum length of the Entry's text.
    /// </summary>
    public class MinLengthValidatorBehavior : Behavior<Entry>
    {
        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(MinLengthValidatorBehavior), false);
        private static readonly BindableProperty ValidNumberTextColorProperty = BindableProperty.Create(nameof(ValidNumberTextColor), typeof(Color), typeof(MinLengthValidatorBehavior), Color.Black);
        private static readonly BindableProperty InvalidNumberTextColorProperty = BindableProperty.Create(nameof(InvalidNumberTextColor), typeof(Color), typeof(MinLengthValidatorBehavior), Color.Red);
        public static readonly BindableProperty MinLengthProperty = BindableProperty.Create(nameof(MinLength), typeof(int), typeof(MinLengthValidatorBehavior), 1);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

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

        public int MinLength
        {
            get => (int)GetValue(MinLengthProperty);
            set => SetValue(MinLengthProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length >= MinLength)
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