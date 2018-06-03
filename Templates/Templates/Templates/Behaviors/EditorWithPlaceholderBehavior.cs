﻿using System;
using Templates.Controls;
using Xamarin.Forms;

namespace Templates.Behaviors
{
    /// <inheritdoc />
    /// <summary>
    /// Provide behavior to EditorWithPlaceholder control.
    /// <para />
    /// Attach this behavior to EditorWithPlaceholder control in XAML.
    /// </summary>
    public class EditorWithPlaceholderBehavior : Behavior<EditorWithPlaceholder>
    {
        protected override void OnAttachedTo(EditorWithPlaceholder editor)
        {
            editor.BindingContextChanged += OnBindingContextChanged;
            editor.Focused += OnEditorFocused;
            editor.Unfocused += OnEditorUnFocused;
            base.OnAttachedTo(editor);

            if (string.IsNullOrEmpty(editor.Text))
            {
                editor.Text = editor.Placeholder;
                editor.TextColor = editor.PlaceholderColor;
            }
        }

        protected override void OnDetachingFrom(EditorWithPlaceholder editor)
        {
            editor.BindingContextChanged -= OnBindingContextChanged;
            editor.Focused -= OnEditorFocused;
            editor.Unfocused -= OnEditorUnFocused;
            base.OnDetachingFrom(editor);
        }

        private void OnBindingContextChanged(object sender, EventArgs args)
        {
            var editor = sender as EditorWithPlaceholder;
            if (string.IsNullOrEmpty(editor.Text))
            {
                editor.Text = editor.Placeholder;
                editor.TextColor = editor.PlaceholderColor;
            }
        }

        private void OnEditorFocused(object sender, FocusEventArgs args)
        {
            var editor = sender as EditorWithPlaceholder;
            string placeholder = editor.Placeholder;
            string text = editor.Text;

            if (placeholder == text)
            {
                editor.Text = string.Empty;
            }
            editor.TextColor = Color.Default;
        }

        private void OnEditorUnFocused(object sender, FocusEventArgs args)
        {
            var editor = sender as EditorWithPlaceholder;
            string placeholder = editor.Placeholder;
            string text = editor.Text;

            if (string.IsNullOrEmpty(text))
            {
                editor.Text = placeholder;
                editor.TextColor = editor.PlaceholderColor;
            }
            else
            {
                editor.TextColor = Color.Default;
            }
        }
    }
}