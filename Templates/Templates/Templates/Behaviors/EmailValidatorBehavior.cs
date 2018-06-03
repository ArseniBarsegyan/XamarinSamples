using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Templates.Behaviors
{
    /// <summary>
    /// Entry email validator behavior.
    /// </summary>
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        private const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        internal static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(nameof(IsValid), typeof(bool), typeof(EmailValidatorBehavior), false);
        public static readonly BindableProperty ValidTextColorHexProperty = BindableProperty.Create(nameof(ValidTextColorHex), typeof(Color), typeof(EmailValidatorBehavior), Color.DodgerBlue);
        public static readonly BindableProperty InvalidTextColorHexProperty = BindableProperty.Create(nameof(InvalidTextColorHex), typeof(Color), typeof(EmailValidatorBehavior), Color.Red);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public Color ValidTextColorHex
        {
            get => (Color)GetValue(ValidTextColorHexProperty);
            set => SetValue(ValidTextColorHexProperty, value);
        }

        public Color InvalidTextColorHex
        {
            get => (Color)GetValue(InvalidTextColorHexProperty);
            set => SetValue(InvalidTextColorHexProperty, value);
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null) return;
            IsValid = Regex.IsMatch(e.NewTextValue, EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            ((Entry)sender).TextColor = IsValid ? ValidTextColorHex : InvalidTextColorHex;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}