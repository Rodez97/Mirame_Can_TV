using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MirameCanTV.Droid.Implementaciones;
using MirameCanTV.InterF;


[assembly: Xamarin.Forms.Dependency(typeof(CloseApplicationImplementation))]
namespace MirameCanTV.Droid.Implementaciones
{
    class CloseApplicationImplementation : ICloseApplication
    {
        public void closeApplication()
        {
            var activity = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity as MainActivity;
            activity.FinishAffinity();
        }
    }
}