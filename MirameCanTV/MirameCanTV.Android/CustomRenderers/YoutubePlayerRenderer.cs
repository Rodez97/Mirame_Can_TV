using Android.Content;
using Android.Widget;
using MirameCanTV.Componentes;
using MirameCanTV.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ARelativeLayout = Android.Widget.RelativeLayout;
using Com.Pierfrancescosoffritti.Androidyoutubeplayer.Core.Player.Views;
using Com.Pierfrancescosoffritti.Androidyoutubeplayer.Core.Player;
using Com.Pierfrancescosoffritti.Androidyoutubeplayer.Core.Player.Listeners;
using System.Threading.Tasks;
using System.ComponentModel;
using System;

[assembly: ExportRenderer(typeof(YoutubePlayer), typeof(YoutubePlayerRenderer))]
namespace MirameCanTV.Droid.CustomRenderers
{
    public class YoutubePlayerRenderer : ViewRenderer<YoutubePlayer, ARelativeLayout>, IYouTubePlayerListener, IYouTubePlayerFullScreenListener
    {
        YouTubePlayerView youTubePlayerView;
        YoutubePlayer YoutubeXamarin;
        IYouTubePlayer tubePlayer;
        
        public YoutubePlayerRenderer(Context context) : base(context)
        {
        }
        protected async override void OnElementChanged(ElementChangedEventArgs<YoutubePlayer> args)
        {
            base.OnElementChanged(args);
            YoutubeXamarin = args.NewElement;

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity as MainActivity;
                    
                    // Save the VideoView for future reference
                    youTubePlayerView = new YouTubePlayerView(Context);

                    // Put the VideoView in a RelativeLayout
                    ARelativeLayout relativeLayout = new ARelativeLayout(Context);
                    relativeLayout.AddView(youTubePlayerView);

                    // Center the VideoView in the RelativeLayout
                    ARelativeLayout.LayoutParams layoutParams = new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                    layoutParams.AddRule(LayoutRules.AlignParentTop);
                    youTubePlayerView.LayoutParameters = layoutParams;
                    youTubePlayerView.PlayerUiController.ShowYouTubeButton(false);
                    youTubePlayerView.PlayerUiController.ShowFullscreenButton(YoutubeXamarin.FullscrennEnable);
                    // Use the RelativeLayout as the native control
                    SetNativeControl(relativeLayout);
                    youTubePlayerView.AddYouTubePlayerListener(this);
                    youTubePlayerView.AddFullScreenListener(this);
                    while (tubePlayer == null)
                    {
                        await Task.Delay(100);
                    }
                    //tubePlayer.LoadVideo("fBg7xDeSxh0", 0);
                }
            }
        }
        public void PlayNewVideo(string idVid)
        {
            tubePlayer.LoadVideo(idVid, 0);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == "VideoSource")
            {
                PlayNewVideo(YoutubeXamarin.VideoSource);
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            youTubePlayerView.Release();
        }
        
        public void OnApiChange(IYouTubePlayer youTubePlayer)
        {
            return;
        }

        public void OnCurrentSecond(IYouTubePlayer youTubePlayer, float second)
        {
            return;
        }

        public void OnError(IYouTubePlayer youTubePlayer, PlayerConstants.PlayerError error)
        {
            return;
        }

        public void OnPlaybackQualityChange(IYouTubePlayer youTubePlayer, PlayerConstants.PlaybackQuality playbackQuality)
        {
            return;
        }

        public void OnPlaybackRateChange(IYouTubePlayer youTubePlayer, PlayerConstants.PlaybackRate playbackRate)
        {
            return;
        }

        public void OnReady(IYouTubePlayer youTubePlayer)
        {
            tubePlayer = youTubePlayer;
            PlayNewVideo(YoutubeXamarin.VideoSource);
        }

        public void OnStateChange(IYouTubePlayer youTubePlayer, PlayerConstants.PlayerState state)
        {
            YoutubeXamarin.Estado = state.Name();
            return;
        }

        public void OnVideoDuration(IYouTubePlayer youTubePlayer, float duration)
        {
            return;
        }

        public void OnVideoId(IYouTubePlayer youTubePlayer, string videoId)
        {
            return;
        }

        public void OnVideoLoadedFraction(IYouTubePlayer youTubePlayer, float loadedFraction)
        {
            return;
        }

        public void OnYouTubePlayerEnterFullScreen()
        {
            YoutubeXamarin.SetFullscreen(true);
        }

        public void OnYouTubePlayerExitFullScreen()
        {
            YoutubeXamarin.SetFullscreen(false);
        }
    }
    
}