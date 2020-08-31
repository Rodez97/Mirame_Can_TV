using MediaManager;
using MediaManager.Forms;
using MediaManager.Player;
using MirameCanTV.InterF;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MirameCanTV.Componentes
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReproductorPersonalizado : Grid
    {
        #region Propiedades del Reproductor
        public static BindableProperty IsFullscreenProperty = BindableProperty.Create(nameof(IsFullscreen), typeof(bool), typeof(ReproductorPersonalizado), false);
        public static BindableProperty IsMutedProperty = BindableProperty.Create(nameof(IsMuted), typeof(bool), typeof(ReproductorPersonalizado), false);
        public static BindableProperty IsStreamProperty = BindableProperty.Create(nameof(IsStream), typeof(bool), typeof(ReproductorPersonalizado), true);
        public static BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(MediaPlayerState), typeof(ReproductorPersonalizado), MediaPlayerState.Stopped);
        public bool IsFullscreen
        {
            get { return (bool)GetValue(IsFullscreenProperty); }
            set { SetValue(IsFullscreenProperty, value); }
        }
        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set 
            { 
                SetValue(IsMutedProperty, value);
                CrossMediaManager.Current.Volume.Muted = value;
            }
        }
        public bool IsStream
        {
            get { return (bool)GetValue(IsStreamProperty); }
            set 
            { 
                SetValue(IsStreamProperty, value);
                //polling = value;
            }
        }
        public MediaPlayerState State
        {
            get { return (MediaPlayerState)GetValue(StateProperty); }
            set
            {
                SetValue(StateProperty, value);
            }
        }
        #endregion
        IMediaManager mm;
        YoutubePlayer youtubePlayer;
        VideoView tvPlayer;
        RefreshView VideoTVfull;
        //bool polling = true;
        string Titulo = "Emisión en directo.";
        public ReproductorPersonalizado()
        {
            InitializeComponent();
            mm = CrossMediaManager.Current;
            mm.StateChanged += Mm_StateChanged;
            tvPlayer = Player;
            VideoTVfull = CargaVideoTV;
            ReproducirVideo("https://593fa17dec176.streamlock.net:455/mirametv/mirametv/master.m3u8");
            DetectarAnchoVideo();
        }
        #region Funciones del reproductor de YouTube
        /// <summary>
        /// Inicializar/Reproducir videos de youtube
        /// </summary>
        /// <param name="idVideo">ID del video a reproducir</param>
        public void ReproducirYoutube(string idVideo)
        {
            if (IsStream)
            {
                IsStream = false;
                GridVideo.Children.RemoveAt(1);
                mm.StateChanged -= Mm_StateChanged;
                mm.Stop();
                mm.Dispose();
                youtubePlayer = new YoutubePlayer { VideoSource = idVideo };
                youtubePlayer.PropertyChanged += YoutubePlayer_PropertyChanged;
                GridVideo.Children.Insert(1, youtubePlayer);
                Controles.InputTransparent = true;
                Controles.Opacity = 0;
                Controles.IsEnabled = false;
                DeviceDisplay.KeepScreenOn = true;
            }
            else if (youtubePlayer != null)
            {
                youtubePlayer.SetSource(idVideo);
            }
        }
        /// <summary>
        /// Controla cuando cambia el State del video de Youtube,
        /// y mantiene o no la pantalla encendida
        /// </summary>
        private void YoutubePlayer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Estado")
            {
                if ((sender as YoutubePlayer).Estado == "Playing")
                {
                    DeviceDisplay.KeepScreenOn = true;
                }
                else
                {
                    DeviceDisplay.KeepScreenOn = false;
                }
            }
        }
        #endregion

        #region Funciones del reproductor nativo (TV)
        /// <summary>
        /// Reproducir la señal en directo (TV)
        /// </summary>
        public void ReproducirTV()
        {
            if (!IsStream)
            {
                IsStream = true;
                GridVideo.Children.RemoveAt(1);
                GridVideo.Children.Insert(1, CargaVideoTV);
                mm.StateChanged += Mm_StateChanged;
                if(youtubePlayer != null) { youtubePlayer.PropertyChanged -= YoutubePlayer_PropertyChanged; }
                ReproducirVideo("https://593fa17dec176.streamlock.net:455/mirametv/mirametv/master.m3u8");
                Controles.InputTransparent = false;
            }
            else
            {
                mm.Play();
            }
        }
        /// <summary>
        /// Controla cuando cambia el State del video de TV,
        /// Establece si el video esta cargando o no, 
        /// y mantiene o no la pantalla encendida
        /// </summary>
        private void Mm_StateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            State = e.State;
            if (e.State == MediaPlayerState.Playing || e.State == MediaPlayerState.Paused)
            {
                VideoTVfull.IsRefreshing = false;
            }
            else
            {
                VideoTVfull.IsRefreshing = true;
            }
            if (e.State == MediaPlayerState.Playing)
            {
                DeviceDisplay.KeepScreenOn = true;
            }
            else
            {
                DeviceDisplay.KeepScreenOn = false;
            }
        }
        /// <summary>
        /// Evento llamado cuando se pulsa el reproductor
        /// </summary>
        private void Reproductor_Tapped(object sender, EventArgs e)
        {
            if (IsStream) { ShowHideControls(); }
        }
        /// <summary>
        /// Mostrar/Ocultar controles del video (Solo TV)
        /// </summary>
        private async void ShowHideControls()
        {
            if (Controles.Opacity == 0)
            {
                Controles.Opacity = 0.8;
                Controles.IsEnabled = true;
                await Task.Delay(5000);
                Controles.IsEnabled = false;
                Controles.Opacity = 0;
            }
            else
            {
                Controles.Opacity = 0;
                Controles.IsEnabled = false;
            }
        }
        /// <summary>
        /// Evento llamado cuando se pulsa Maximizar/Minimizar
        /// </summary>
        private void Maximizar_Clicked(object sender, EventArgs e)
        {
            MaximizarReproductor();
        }
        /// <summary>
        /// Evento llamado cuando se pulsa el botón de Play/Pause (Solo TV)
        /// </summary>
        private void Play_Clicked(object sender, EventArgs e)
        {
            if (mm.State == MediaPlayerState.Stopped || mm.State == MediaPlayerState.Paused)
            {
                mm.Play();
            }
            else if (mm.State == MediaPlayerState.Playing)
            {
                mm.Pause();
            }
        }
        /// <summary>
        /// Reproducir video / stream (En caso de "emergencia")
        /// </summary>
        /// <param name="URLvid">ID para reproducir los videos de forma alternativa</param>
        private async void ReproducirVideo(string URLvid)
        {
            await mm.Play(URLvid);
            mm.Queue.Current.DisplayTitle = Titulo;
            mm.Queue.Current.DisplaySubtitle = "Mírame TV";
            mm.Queue.Current.DisplayImageUri = "SoloLogoColorLetra.png";
            mm.Queue.Current.DisplayImage = "SoloLogoColorLetra.png";
            mm.Notification.UpdateNotification();
        }
        #endregion

        #region Funciones generales del reproductor
        /// <summary>
        /// Establece el espacio que ocupa el reproductor
        /// </summary>
        private void DetectarAnchoVideo()
        {
            var AspectRatio = (double)0.5625;
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            // Orientation (Landscape, Portrait, Square, Unknown)
            var orientation = mainDisplayInfo.Orientation;
            // Width (in xamarin.forms units)
            var xamarinWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
            switch (orientation)
            {
                case DisplayOrientation.Landscape:

                    GridPlayer.HeightRequest = -1;
                    break;
                case DisplayOrientation.Portrait:
                    FondoVideo.HeightRequest = xamarinWidth * AspectRatio;
                    break;
                default:
                    GridPlayer.HeightRequest = 200;
                    break;
            }
        }
        /// <summary>
        /// Función que se encarga de Maximizar/Minimizar
        /// </summary>
        public void MaximizarReproductor()
        {
            var PagePadre = (this.Parent.Parent as MainPage);//MainPage
            if (!IsFullscreen)
            {
                //Maximizar
                NavigationPage.SetHasNavigationBar(PagePadre, false);
                DependencyService.Get<IStatusBar>().HideStatusBar();
                (PagePadre.FindByName("ColeccionPrincipal") as CollectionView).IsVisible = false;
                VerticalOptions = LayoutOptions.FillAndExpand;
                GridPlayer.HeightRequest = -1;
                FondoVideo.HeightRequest = -1;
                GridPlayer.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                DependencyService.Get<IOrientacion>().PermitirOrientacion();
            }
            else
            {
                //Minimizar
                DependencyService.Get<IOrientacion>().BloquearOrientacion();
                GridPlayer.RowDefinitions[0].Height = GridLength.Auto;
                VerticalOptions = LayoutOptions.Start;
                DetectarAnchoVideo();
                (PagePadre.FindByName("ColeccionPrincipal") as CollectionView).IsVisible = true;
                DependencyService.Get<IStatusBar>().ShowStatusBar();
                NavigationPage.SetHasNavigationBar(PagePadre, true);
            }
            IsFullscreen = IsFullscreen ? false : true;
        }
        #endregion

        #region Funciones opcionales o de "¿Emergencia?"/Obsoletas pero necesarias
        
        /// <summary>
        /// Iniciar barra de posición del video
        /// </summary>
        //[Obsolete]
        //private void IniciarPosicionBar()
        //{
        //    Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
        //    {
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            positionSlider.Value = mm.Position.TotalSeconds;
        //        });
        //        return polling;
        //    });
        //}
        /// <summary>
        /// Detener barra de posición del video
        /// </summary>
        //[Obsolete]
        //private void DetenerPosicionBar()
        //{
        //    polling = false;
        //}
        /// <summary>
        /// Función encargada de actualizar la barra posicional (Solo Player Nativo)
        /// </summary>
        //[Obsolete]
        //private void positionSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    if ((e.NewValue - e.OldValue) >= 10)
        //    {
        //        mm.SeekTo(TimeSpan.FromSeconds(e.NewValue));
        //    }
        //}
        #endregion
















    }
}