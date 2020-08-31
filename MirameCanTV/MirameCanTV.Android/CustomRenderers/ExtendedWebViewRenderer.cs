using Android.Content;
using Android.Webkit;
using MirameCanTV.Componentes;
using MirameCanTV.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(ExtendedWebViewRenderer))]
namespace MirameCanTV.Droid.CustomRenderers
{
    public class ExtendedWebViewRenderer : WebViewRenderer
    {
        Context _context;
        public ExtendedWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }
        static CustomWebView _xwebView = null;
        Android.Webkit.WebView _webView;

        class ExtendedWebViewClient : WebViewClient
        {
            public override async void OnPageFinished(Android.Webkit.WebView view, string url)
            {
                if (_xwebView != null)
                {
                    for(int i = 0; 0 < 10; i++)
                    {
                        if(view.ContentHeight != 0)
                        {
                            //_xwebView.HeightRequest = view.ContentHeight;
                            _xwebView.HeightWV = view.ContentHeight;
                            break;
                        }
                        else
                        {
                            await System.Threading.Tasks.Task.Delay(100);
                        }
                    }
                }
                base.OnPageFinished(view, url);
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            _xwebView = e.NewElement as CustomWebView;
            _webView = Control;

            if (e.OldElement == null)
            {
                _webView.SetWebViewClient(new ExtendedWebViewClient());
            }
        }
    }
}