using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace MirameCanTV.iOS.CustomRenderers
{
    public class LogoPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var image = UIImage.FromBundle("logo.png");
            var imageView = new UIImageView(new CGRect(0, 0, 140, 70));

            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            imageView.Image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);

            if (NavigationController != null)
            {
                NavigationController.TopViewController.NavigationItem.TitleView = imageView;
            }
        }
    }
}