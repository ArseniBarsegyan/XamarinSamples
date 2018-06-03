﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using Templates.Services;
using Templates.iOS.Services;
using CoreGraphics;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AlertServiceIos))]
namespace Templates.iOS.Services
{
    /// <summary>
    /// Implementation of <see cref="IAlertService"/>
    /// </summary>
    public class AlertServiceIos : UIViewController, IAlertService
    {
        public AlertServiceIos()
        {
        }

        public void ShowAlert(string message)
        {
            var tcs = new TaskCompletionSource<int>();
            var alert = new UIAlertView
            {
                Title = "Alert",
                Message = message
            };

            alert.AddButton("Ok");
            alert.Show();
        }

        public Task<bool> ShowResolution(string message)
        {

            var tcs = new TaskCompletionSource<bool>();
            var alert = new UIAlertView
            {
                Title = "Alert",
                Message = message
            };

            alert.AddButton("Ok");
            alert.AddButton("Cancel");
            alert.Clicked += (s, e) => { tcs.TrySetResult(e.ButtonIndex == 0); };
            alert.Show();
            return tcs.Task;
        }

        public void ShowOkAlert(string message, string okButtonText)
        {
            var popupOverlay = new CustomPopUpView(message, okButtonText, new CGSize(300, 150), isWarning: false);
            popupOverlay.ShowPopUp(true, null);
        }

        public Task<bool> ShowUploadAlert(string message, string okButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var popupOverlay = new CustomPopUpView(message, okButtonText, new CGSize(300, 150));
            popupOverlay.YesButtonClicked += () =>
            {
                tcs.TrySetResult(true);
            };
            popupOverlay.NoButtonClicked += () =>
            {
                tcs.TrySetResult(false);
            };
            popupOverlay.ShowPopUp(true, null);
            return tcs.Task;
        }

        public Task<bool> ShowYesNoAlert(string message, string yesButtonText, string noButtonText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var popupOverlay = new CustomPopUpView(message, yesButtonText, noButtonText, new CGSize(300, 150));
            popupOverlay.YesButtonClicked += () =>
            {
                tcs.TrySetResult(true);
            };
            popupOverlay.NoButtonClicked += () =>
            {
                tcs.TrySetResult(false);
            };
            popupOverlay.ShowPopUp(true, null);
            return tcs.Task;
        }
        public void ShowWarningAlert(string message, string warningButtonText)
        {
            var popupOverlay = new CustomPopUpView(message, warningButtonText, new CGSize(300, 150), isWarning: true);
            popupOverlay.ShowPopUp(true, null);
        }
    }

    public class CustomPopUpView : UIView
    {
        public delegate void PopWillCloseHandler();
        public event PopWillCloseHandler PopWillClose;

        public delegate void YesButtonEventHandler();
        public event YesButtonEventHandler YesButtonClicked;

        public delegate void NoButtonEventHandler();
        public event NoButtonEventHandler NoButtonClicked;

        private UIVisualEffectView _effectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark));
        private UIButton _btnClose = new UIButton(UIButtonType.Custom);
        private UIButton _actionBtn = new UIButton(UIButtonType.Custom);
        private const float _cornerRadius = 10;
        private nfloat _btnHeight = 40;

        public CustomPopUpView(string message, string buttonText, CGSize size, bool isWarning = false)
        {
            nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width) / 2;
            nfloat ly = (UIScreen.MainScreen.Bounds.Height - size.Height) / 2;
            UIImageView img = null;
            this.Frame = new CGRect(new CGPoint(lx, ly), size);
            this.Layer.CornerRadius = _cornerRadius;
            this.Layer.MasksToBounds = true;
            _effectView.Alpha = 0;

            this.BackgroundColor = UIColor.White;

            if (isWarning)
            {
                img = new UIImageView(UIImage.FromBundle("InfoIcon.png"));
            }
            else
            {
                img = new UIImageView(UIImage.FromBundle("OkIcon.png"));
            }

            img.Frame = new CoreGraphics.CGRect(10, 40, img.Image.CGImage.Width / 2, img.Image.CGImage.Height / 2);
            string messageBody;
            if (message.ToCharArray().Length < 28)
            {
                messageBody = string.IsNullOrEmpty(message) ? "Error" : message;
            }
            else
            {
                messageBody = string.IsNullOrEmpty(message + "\n") ? "Error" : message + "\n";
            }
            var label = new UILabel()
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Black,
                Text = messageBody,
                TextAlignment = UITextAlignment.Left,
                AutoresizingMask = UIViewAutoresizing.All,
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };
            var labelSize = label.Text.StringSize(label.Font, new SizeF((float)this.Frame.Width, 500));

            label.Frame = new CGRect((img.Image.CGImage.Width / 2) + 20, labelSize.Height > 40 ? 35 : 50, 200, labelSize.Height);

            var stack = new UIStackView()
            {
                Alignment = UIStackViewAlignment.Center
            };
            stack.Frame = new CGRect(0, 0, this.Frame.Width, this.Frame.Height);

            _actionBtn.SetTitle(buttonText, UIControlState.Normal);
            _actionBtn.Frame = new CGRect(0, this.Frame.Height - _btnHeight, this.Frame.Width, _btnHeight);
            _actionBtn.BackgroundColor = UIColor.FromRGB(4, 103, 161);
            _actionBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
            _actionBtn.TouchUpInside += delegate
            {
                YesButtonClicked?.Invoke();
                Close();
            };

            stack.AddSubview(label);
            stack.AddSubview(img);
            stack.AddSubview(_actionBtn);
            this.AddSubview(stack);
        }

        public CustomPopUpView(string message, string buttonText, CGSize size)
        {
            UIImageView img = null;
            this.Layer.CornerRadius = _cornerRadius;
            this.Layer.MasksToBounds = true;
            _effectView.Alpha = 0;

            this.BackgroundColor = UIColor.White;

            img = new UIImageView(UIImage.FromBundle("UploadDocumentIcon.png"));

            img.Frame = new CoreGraphics.CGRect(10, 40, img.Image.CGImage.Width / 2, img.Image.CGImage.Height / 2);
            var messageBody = string.IsNullOrEmpty(message + "\n") ? "Error" : message + "\n";
            var messageLabel = new UILabel()
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Black,
                Text = messageBody,
                TextAlignment = UITextAlignment.Left,
                AutoresizingMask = UIViewAutoresizing.All,
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };

            var messageLabelSize = messageLabel.Text.StringSize(messageLabel.Font, new SizeF((float)size.Width, 500));

            var messageLabelHeight = messageLabelSize.Height + 20;
            messageLabel.Frame = new CGRect((img.Image.CGImage.Width / 2) + 20, 30, 220, messageLabelHeight);

            var additionalFrameHeight = (messageLabelHeight - size.Height) < 0 ? 90 + (messageLabelHeight - size.Height) : (messageLabelHeight - size.Height) + 90;

            nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width) / 2;
            nfloat ly = (UIScreen.MainScreen.Bounds.Height - (size.Height + additionalFrameHeight)) / 2;

            this.Frame = new CGRect(new CGPoint(lx, ly), new CGSize(size.Width, size.Height + additionalFrameHeight));

            var stack = new UIStackView()
            {
                Alignment = UIStackViewAlignment.Center
            };
            stack.Frame = new CGRect(0, 0, this.Frame.Width, this.Frame.Height);

            _actionBtn.SetTitle(buttonText, UIControlState.Normal);
            _actionBtn.Frame = new CGRect(0, this.Frame.Height - _btnHeight, this.Frame.Width, _btnHeight);
            _actionBtn.BackgroundColor = UIColor.FromRGB(4, 103, 161);
            _actionBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
            _actionBtn.TouchUpInside += delegate
            {
                YesButtonClicked?.Invoke();
                Close();
            };

            stack.AddSubview(messageLabel);
            stack.AddSubview(img);
            stack.AddSubview(_actionBtn);
            this.AddSubview(stack);
        }

        public CustomPopUpView(string message, string yesButtonText, string noButtonText, CGSize size)
        {
            nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width) / 2;
            nfloat ly = (UIScreen.MainScreen.Bounds.Height - size.Height) / 2;
            this.Frame = new CGRect(new CGPoint(lx, ly), size);
            this.Layer.CornerRadius = _cornerRadius;
            this.Layer.MasksToBounds = true;
            _effectView.Alpha = 0;

            this.BackgroundColor = UIColor.White;

            var img = new UIImageView(UIImage.FromBundle("QuestionIcon.png"));
            img.Frame = new CoreGraphics.CGRect(10, 40, img.Image.CGImage.Width / 2, img.Image.CGImage.Height / 2);
            var messageBody = string.IsNullOrEmpty(message + "\n") ? "Error" : message + "\n";
            var label = new UILabel()
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Black,
                Text = messageBody,
                TextAlignment = UITextAlignment.Left,
                AutoresizingMask = UIViewAutoresizing.All,
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };
            var labelSize = label.Text.StringSize(label.Font, new SizeF((float)this.Frame.Width, 500));
            var labelHeight = labelSize.Height + 25;

            label.Frame = new CGRect((img.Image.CGImage.Width / 2) + 20, labelHeight > 25 ? 30 : 50, 200, labelHeight);

            var stack = new UIStackView()
            {
                Alignment = UIStackViewAlignment.Center
            };
            stack.Frame = new CGRect(0, 0, this.Frame.Width, this.Frame.Height);

            _btnClose.SetTitle(noButtonText, UIControlState.Normal);
            _btnClose.Frame = new CGRect(this.Frame.Width / 2, this.Frame.Height - _btnHeight, this.Frame.Width / 2, _btnHeight);
            _btnClose.BackgroundColor = UIColor.FromRGB(245, 245, 245);
            _btnClose.SetTitleColor(UIColor.FromRGB(4, 103, 161), UIControlState.Normal);
            _btnClose.TouchUpInside += delegate
            {
                NoButtonClicked?.Invoke();
                Close();
            };

            _actionBtn.SetTitle(yesButtonText, UIControlState.Normal);
            _actionBtn.Frame = new CGRect(0, this.Frame.Height - _btnHeight, this.Frame.Width / 2, _btnHeight);
            _actionBtn.BackgroundColor = UIColor.FromRGB(4, 103, 161);
            _actionBtn.SetTitleColor(UIColor.White, UIControlState.Normal);
            _actionBtn.TouchUpInside += delegate
            {
                YesButtonClicked?.Invoke();
                Close();
            };

            stack.AddSubview(label);
            stack.AddSubview(img);
            stack.AddSubview(_btnClose);
            stack.AddSubview(_actionBtn);
            this.AddSubview(stack);
        }

        public void ShowPopUp(bool animated = true, Action popAnimationFinish = null)
        {
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            _effectView.Frame = window.Bounds;

            UITapGestureRecognizer tapGesture = null;
            Action action = () =>
            {
                NoButtonClicked?.Invoke();
                Close();
            };

            tapGesture = new UITapGestureRecognizer(action);
            tapGesture.NumberOfTapsRequired = 1;
            _effectView.AddGestureRecognizer(tapGesture);

            window.EndEditing(true);
            window.AddSubview(_effectView);
            window.AddSubview(this);

            if (animated)
            {
                Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
                UIView.Animate(0.15, delegate
                {
                    Transform = CGAffineTransform.MakeScale(1, 1);
                    _effectView.Alpha = 0.8f;
                }, delegate
                {
                    popAnimationFinish?.Invoke();
                });
            }
            else
            {
                _effectView.Alpha = 0.8f;
            }
        }

        public void Close(bool animated = true)
        {
            if (animated)
            {
                UIView.Animate(0.15, delegate
                {
                    Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
                    _effectView.Alpha = 0;
                }, delegate
                {
                    this.RemoveFromSuperview();
                    _effectView.RemoveFromSuperview();
                    PopWillClose?.Invoke();
                });
            }
            else
            {
                PopWillClose?.Invoke();
            }
        }
    }
}