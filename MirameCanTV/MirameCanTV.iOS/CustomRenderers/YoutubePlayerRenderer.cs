using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using MirameCanTV.Componentes;
using MirameCanTV.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using YouTube.Player;

[assembly: ExportRenderer(typeof(YoutubePlayer), typeof(YoutubePlayerRenderer))]
namespace MirameCanTV.iOS.CustomRenderers
{
    public class YoutubePlayerRenderer : ViewRenderer<YoutubePlayer, UIView>
    {
        PlayerView youTubePlayerView;
        YoutubePlayer YoutubeXamarin;
        IPlayerViewDelegate tubePlayer;

        protected override void OnElementChanged(ElementChangedEventArgs<YoutubePlayer> args)
        {
            base.OnElementChanged(args);
            YoutubeXamarin = args.NewElement;

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    // Create new Player
                    youTubePlayerView = new PlayerView();

                    // Set Player Controller
                    tubePlayer = youTubePlayerView.Delegate;
                    youTubePlayerView.BecameReady += YouTubePlayerView_BecameReady;
                    youTubePlayerView.StateChanged += YouTubePlayerView_StateChanged;

                    // Use the View from the controller as the native control
                    SetNativeControl(youTubePlayerView);
                }
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == "VideoSource")
            {
                youTubePlayerView.LoadVideoById(YoutubeXamarin.VideoSource);
                youTubePlayerView.PlayVideo();
            }
        }

        private void YouTubePlayerView_StateChanged(object sender, PlayerViewStateChangedEventArgs e)
        {
            YoutubeXamarin.Estado = e.State.ToString();
        }

        private void YouTubePlayerView_BecameReady(object sender, EventArgs e)
        {
            youTubePlayerView.LoadVideoById(YoutubeXamarin.VideoSource);
            youTubePlayerView.PlayVideo();
        }
    }
}