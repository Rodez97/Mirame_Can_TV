using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MirameCanTV.Componentes;
using MirameCanTV.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(ExtendedWebViewRenderer))]
namespace MirameCanTV.iOS.CustomRenderers
{
    public class ExtendedWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            Delegate = new ExtendedWebViewRendererDelegate(this);
        }
    }
    public class ExtendedWebViewRendererDelegate : UIWebViewDelegate
    {
        ExtendedWebViewRenderer webViewRenderer;

        public ExtendedWebViewRendererDelegate(ExtendedWebViewRenderer _webViewRenderer = null)
        {
            webViewRenderer = _webViewRenderer ?? new ExtendedWebViewRenderer();
        }
        public override async void LoadingFinished(UIWebView webView)
        {
            var wv = webViewRenderer.Element as CustomWebView;
            if (wv != null)
            {
                await System.Threading.Tasks.Task.Delay(100); // wait here till content is rendered
                wv.HeightRequest = (double)webView.ScrollView.ContentSize.Height;
            }
        }
    }
}