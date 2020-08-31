using MirameCanTV.Clases;
using MirameCanTV.Models;
using MirameCanTV.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace MirameCanTV
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        PancakeView SelectedItem;
        /// <summary>
        /// Constructor de la página Principal de contenido
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            CreateElementos("");
            Repro.PropertyChanged += Repro_PropertyChanged;
        }

        private void Repro_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsStream")
            {
                CambiarYTTV();
            }
        }

        public async void CreateElementos(string PlaylistID)
        {
            List<ItemModel> source = new List<ItemModel>();
            await Task.Run(async () =>
            {
                source = await ParseFeedContent.ParseFeedYoutube(PlaylistID);
            });

            ColeccionPrincipal.ItemsSource = source;
        }

        private async void Contactanos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactPage());
        }

        private async void Configuracion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        public void backToStream()
        {
            if (!Repro.IsStream) { Repro.ReproducirTV(); }
        }

        private void backToStream_Clicked(object sender, EventArgs e)
        {
            backToStream();
        }

        private void CambiarYTTV()
        {
            ToolbarItems[2].IconImageSource = Repro.IsStream ? "TV_Red.png" : "TV_White.png";
        }

        private void Categorias_Tapped(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                SelectedItem = (sender as PancakeView);
                SelectedItem.BackgroundGradientStops = 
                    new GradientStopCollection { new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 1 }, new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 0.5f }, new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 0 } };
            }
            else
            {
                SelectedItem.BackgroundGradientStops =
                    new GradientStopCollection { new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 1 }, new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 0.5f }, new GradientStop { Color = Color.FromHex("#FFC9A4"), Offset = 0 } };
                SelectedItem = (sender as PancakeView);
                SelectedItem.BackgroundGradientStops =
                    new GradientStopCollection { new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 1 }, new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 0.5f }, new GradientStop { Color = Color.FromHex("#FFAA70"), Offset = 0 } };
            }
            CreateElementos(((sender as PancakeView).BindingContext as PlayListModel).Id);
        }

    }
}
