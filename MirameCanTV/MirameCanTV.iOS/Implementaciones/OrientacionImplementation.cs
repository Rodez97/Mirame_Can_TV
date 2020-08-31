using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MirameCanTV.InterF;
using MirameCanTV.iOS.Implementaciones;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(OrientacionImplementation))]
namespace MirameCanTV.iOS.Implementaciones
{
    public class OrientacionImplementation : IOrientacion
    {
        public void BloquearOrientacion()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
        }

        public void PermitirOrientacion()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
        }
    }
}