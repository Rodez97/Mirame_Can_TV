using MediaManager;
using MirameCanTV.Clases;
using MirameCanTV.InterF;
using MirameCanTV.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MirameCanTV
{
    public partial class App : Application
    {
        Page PaginaMenu = new MasterMenu();
        public App()
        {
            
            InitializeComponent();
            DependencyService.Get<IStatusBar>().SetColor(FuncionesAuxiliares.GetHexString((Color)Resources["FondoSecundario"]));

            MainPage = new NavigationPage(PaginaMenu);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            var mm = CrossMediaManager.Current;
            if (mm.State == MediaManager.Player.MediaPlayerState.Playing && !Preferences.Get("PlaySegundoPlano", false))
            {
                CrossMediaManager.Current.Pause();
            }
        }

        protected override void OnResume()
        {
            var mm = CrossMediaManager.Current;
            if (mm.State == MediaManager.Player.MediaPlayerState.Paused && !Preferences.Get("PlaySegundoPlano", false))
            {
                CrossMediaManager.Current.Play();
            }
        }
    }
}
