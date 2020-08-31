using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MirameCanTV.Componentes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YoutubePlayer : View
    {
        #region Propiedades del Player
        // Source Del Video
        public static readonly BindableProperty VideoSourceProperty =
            BindableProperty.Create(nameof(VideoSource), typeof(string), typeof(YoutubePlayer), "urZsxKxG1S4");

        public string VideoSource
        {
            set { SetValue(VideoSourceProperty, value); }
            get { return (string)GetValue(VideoSourceProperty); }
        }
        // State Del Video
        public static readonly BindableProperty EstadoProperty =
            BindableProperty.Create(nameof(Estado), typeof(string), typeof(YoutubePlayer), "Unstarted");

        public string Estado
        {
            set { SetValue(EstadoProperty, value); }
            get { return (string)GetValue(EstadoProperty); }
        }
        // Fullscreen?
        public static readonly BindableProperty FullscrennEnableProperty =
            BindableProperty.Create(nameof(FullscrennEnable), typeof(bool), typeof(YoutubePlayer), true);

        public bool FullscrennEnable
        {
            set { SetValue(FullscrennEnableProperty, value); }
            get { return (bool)GetValue(FullscrennEnableProperty); }
        }
        #endregion
        public YoutubePlayer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Cambiar el video del reproductor
        /// </summary>
        /// <param name="id">ID del video de youtube</param>
        public void SetSource(string id)
        {
            while(Estado == "Buffering" || Estado== "Unknown" || Estado == "Unstarted")
            {
                Task.Delay(100);
            }
            VideoSource = id;
        }
        /// <summary>
        /// Maximizar el reproductor
        /// </summary>
        /// <param name="fullS">¿Maximizar?</param>
        public void SetFullscreen(bool fullS)
        {
            if(Parent.Parent.GetType() == typeof(ReproductorPersonalizado))
            {
                (Parent.Parent as ReproductorPersonalizado).MaximizarReproductor();
            }
        }
    }
}