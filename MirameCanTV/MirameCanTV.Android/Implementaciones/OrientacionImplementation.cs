using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MirameCanTV.Droid.Implementaciones;
using MirameCanTV.InterF;

[assembly: Xamarin.Forms.Dependency(typeof(OrientacionImplementation))]
namespace MirameCanTV.Droid.Implementaciones
{
    public class OrientacionImplementation : IOrientacion
    {
        public OrientacionImplementation() { }
        public void BloquearOrientacion()
        {
            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity as MainActivity;
            activity.RequestedOrientation = ScreenOrientation.Portrait;
        }

        public void PermitirOrientacion()
        {
            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity as MainActivity;
            activity.RequestedOrientation = ScreenOrientation.Sensor;
        }
    }
}