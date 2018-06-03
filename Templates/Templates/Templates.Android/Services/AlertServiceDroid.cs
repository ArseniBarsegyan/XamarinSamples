using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Templates.Droid.Services;
using Templates.Services;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(AlertServiceDroid))]
namespace Templates.Droid.Services
{
    /// <summary>
    /// Implementation of <see cref="IAlertService"/>
    /// </summary>
    public class AlertServiceDroid : IAlertService
    {
        public void ShowAlert(string message)
        {
            var dialog = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert");
            alert.SetMessage(message);
            alert.SetButton("Ok", (c, ev) =>
            {
                alert.Hide();
            });
            alert.Show();
        }

        public Task<bool> ShowResolution(string message)
        {
            var tcs = new TaskCompletionSource<bool>();
            var dialog = new AlertDialog.Builder(CrossCurrentActivity.Current.Activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert");
            alert.SetMessage(message);
            alert.SetButton("Ok", (c, ev) =>
            {
                tcs.SetResult(true);
            });
            alert.SetButton2("Cancel", (c, ev) =>
            {
                tcs.SetResult(false);
            });
            alert.Show();
            return tcs.Task;
        }

        public void ShowOkAlert(string message, string okButtonText)
        {
            var dialog = new Dialog(CrossCurrentActivity.Current.Activity);
            dialog.Window.SetBackgroundDrawable(ContextCompat.GetDrawable(CrossCurrentActivity.Current.Activity, Android.Resource.Color.Transparent));
            dialog.SetContentView(Resource.Layout.CustomAlert);
            TextView text = (TextView)dialog.FindViewById(Resource.Id.alertText);
            text.SetText(message, TextView.BufferType.Normal);

            Button button = (Button)dialog.FindViewById(Resource.Id.alertApplyButton);
            button.SetText(okButtonText, TextView.BufferType.Normal);
            button.Click += (sender, args) =>
            {
                dialog.Dismiss();
            };

            dialog.Show();
        }

        public Task<bool> ShowUploadAlert(string message, string okButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var dialog = new Dialog(CrossCurrentActivity.Current.Activity);
            dialog.Window.SetBackgroundDrawable(ContextCompat.GetDrawable(CrossCurrentActivity.Current.Activity, Android.Resource.Color.Transparent));
            dialog.SetContentView(Resource.Layout.CustomAlert);
            TextView text = (TextView)dialog.FindViewById(Resource.Id.alertText);
            text.SetText(message, TextView.BufferType.Normal);

            ImageView imgView = (ImageView)dialog.FindViewById(Resource.Id.alertImage);
            imgView.SetImageResource(Resource.Drawable.UploadDocumentIcon);

            Button button = (Button)dialog.FindViewById(Resource.Id.alertApplyButton);
            button.SetText(okButtonText, TextView.BufferType.Normal);

            dialog.DismissEvent += (sender, e) =>
            {
                tcs.TrySetResult(false);
            };

            button.Click += (sender, args) =>
            {
                tcs.TrySetResult(true);
                dialog.Dismiss();
            };

            dialog.Show();
            return tcs.Task;
        }

        public Task<bool> ShowYesNoAlert(string message, string yesButtonText, string noButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var dialog = new Dialog(CrossCurrentActivity.Current.Activity);
            dialog.Window.SetBackgroundDrawable(ContextCompat.GetDrawable(CrossCurrentActivity.Current.Activity, Android.Resource.Color.Transparent));
            dialog.SetContentView(Resource.Layout.CustomAlertYesNo);
            TextView text = (TextView)dialog.FindViewById(Resource.Id.yesNoText);
            text.SetText(message, TextView.BufferType.Normal);

            Button acceptButton = (Button)dialog.FindViewById(Resource.Id.yesNoApplyButton);
            acceptButton.SetText(yesButtonText, TextView.BufferType.Normal);

            dialog.DismissEvent += (sender, e) =>
            {
                tcs.TrySetResult(false);
            };

            acceptButton.Click += (sender, e) =>
            {
                tcs.TrySetResult(true);
                dialog.Dismiss();
            };

            Button rejectButton = (Button)dialog.FindViewById(Resource.Id.yesNoRejectButton);
            rejectButton.SetText(noButtonText, TextView.BufferType.Normal);
            rejectButton.Click += (sender, e) =>
            {
                tcs.TrySetResult(false);
                dialog.Dismiss();
            };

            dialog.Show();

            return tcs.Task;
        }

        public void ShowWarningAlert(string message, string warningButtonText)
        {
            var dialog = new Dialog(CrossCurrentActivity.Current.Activity);
            dialog.Window.SetBackgroundDrawable(ContextCompat.GetDrawable(CrossCurrentActivity.Current.Activity, Android.Resource.Color.Transparent));
            dialog.SetContentView(Resource.Layout.CustomAlert);

            TextView text = (TextView)dialog.FindViewById(Resource.Id.alertText);
            text.SetText(message, TextView.BufferType.Normal);

            ImageView imgView = (ImageView)dialog.FindViewById(Resource.Id.alertImage);
            imgView.SetImageResource(Resource.Drawable.InfoIcon);

            Button button = (Button)dialog.FindViewById(Resource.Id.alertApplyButton);
            button.SetText(warningButtonText, TextView.BufferType.Normal);
            dialog.Show();
            button.Click += (sender, args) =>
            {
                dialog.Dismiss();
            };
        }
    }
}