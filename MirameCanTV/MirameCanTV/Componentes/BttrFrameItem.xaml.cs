using FFImageLoading.Forms;
using MirameCanTV.Clases;
using MirameCanTV.Models;
using MirameCanTV.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace MirameCanTV.Componentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BttrFrameItem : PancakeView
    {
        // Objeto Base
        #region Atributos Base binding
        /// <summary>
        /// Bindable property to set the parent bindingcontext.
        /// </summa4ry>
        public static BindableProperty ParentBindingContextProperty = BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(BttrFrameItem), null, propertyChanged: OnParentContextPropertyChanged);
        /// <summary>
        /// Gets or sets the parent bindingcontext.
        /// </summary>
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }
        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as BttrFrameItem).ParentBindingContext = newValue;
            }
        }
        #endregion
        public BttrFrameItem()
        {
            InitializeComponent();
            LoadImageAsync();
        }
        private async void LoadImageAsync()
        {
            try
            {
                await Task.Delay(100);
                string imageURI = (BindingContext as ItemModel).ImageUrlHD;
                Task<ImageSource> result = Task<ImageSource>.Factory.StartNew(() => ImageSource.FromUri(new Uri(imageURI)));
                Imagen.Source = await result;
            }
            catch(Exception error)
            {
                Console.WriteLine(error);
            }
            
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var tt = (BindingContext as ItemModel);
            ((this.Parent.Parent.Parent as StackLayout).Children[0] as ReproductorPersonalizado).ReproducirYoutube(tt.Id);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = (BindingContext as ItemModel).Url,
                Text = (BindingContext as ItemModel).Title,
                Title = "Comparte este Video/Artículo de Mírame TV"
            });
        }
    }
}