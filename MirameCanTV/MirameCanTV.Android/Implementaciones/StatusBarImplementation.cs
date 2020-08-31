using Android.App;
using Android.Views;
using MirameCanTV.Droid.Implementaciones;
using MirameCanTV.InterF;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarImplementation))]
namespace MirameCanTV.Droid.Implementaciones
{
    public class StatusBarImplementation : IStatusBar
    {
        public StatusBarImplementation() { }

        readonly SystemUiFlags _systemUiFlags;

        #region IStatusBar implementación

        public void HideStatusBar()
        {
            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity as MainActivity;

            //====================================
            int uiOptions = (int)activity.Window.DecorView.SystemUiVisibility;

            uiOptions |= (int)SystemUiFlags.LowProfile;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            //====================================
        }

        public void ShowStatusBar()
        {
            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity as MainActivity;

            //====================================
            int uiOptions = (int)activity.Window.DecorView.SystemUiVisibility;

            uiOptions = (int)_systemUiFlags;

            activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            //====================================
        }

        public void SetColor(string hexColor)
        {
            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity as MainActivity;
            activity.SetStatusBarColor(Android.Graphics.Color.ParseColor(hexColor));
        }

        #endregion
    }
}