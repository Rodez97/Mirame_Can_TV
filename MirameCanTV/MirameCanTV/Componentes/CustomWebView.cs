using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MirameCanTV.Componentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class CustomWebView : WebView
    {
        #region Propiedades
        public static BindableProperty HeightWVProperty = BindableProperty.Create(nameof(HeightWV), typeof(double), typeof(CustomWebView), 0.0);
        public double HeightWV
        {
            get { return (double)GetValue(HeightWVProperty); }
            set 
            { 
                SetValue(HeightWVProperty, value);
                this.HeightRequest = value;
            }
        }
        #endregion
        public CustomWebView()
        {
            this.HeightRequest = HeightWV;
        }
    }
}
