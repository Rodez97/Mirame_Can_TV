using System.Threading;
using MirameCanTV.InterF;
using MirameCanTV.iOS.Implementaciones;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApplicationImplementation))]
namespace MirameCanTV.iOS.Implementaciones
{
    class CloseApplicationImplementation : ICloseApplication
    {
        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}