using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace MirameCanTV.Clases
{
    public static class FuncionesAuxiliares
    {
        /// <summary>
        /// Devuelve el valor hexadecimal de un color
        /// </summary>
        /// <param name="color">Color base al que realizar la conversión</param>
        /// <returns>Valor hexadecimal devuelto por la función</returns>
        public static string GetHexString(Color color)
        {
            var red = (int)(color.R * 255);
            var green = (int)(color.G * 255);
            var blue = (int)(color.B * 255);
            //var alpha = (int)(color.A * 255);
            //var hex = $"#{alpha:X2}{red:X2}{green:X2}{blue:X2}";
            var hex = $"#{red:X2}{green:X2}{blue:X2}";

            return hex;
        }
        /// <summary>
        /// Obtener url directa de youtube en caso de ser necesario
        /// </summary>
        /// <param name="videoId">ID del video que desea buscar</param>
        /// <returns></returns>
        public static async Task<string> GetYouTubeUrl(string videoId)
        {
            YoutubeClient Cliente = new YoutubeClient();
            var streamManifest = await Cliente.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();
            string urlStream = streamInfo.Url;
            Cliente = null;
            return @urlStream;
            #region Resguardo
            //var videoInfoUrl = $"https://www.youtube.com/get_video_info?video_id={videoId}";
            //string videoPageContent = string.Empty;
            ////Console.WriteLine(videoInfoUrl);
            //await Task.Run(async () =>
            //{
            //    videoPageContent = await ParseFeedContent.IntentarCargarContenidoWeb(videoInfoUrl);
            //});
            ////Console.WriteLine(videoPageContent);
            //var videoParameters = HttpUtility.ParseQueryString(videoPageContent);
            ////Console.WriteLine(videoParameters);
            //var encodedStreamsDelimited = WebUtility.HtmlDecode(videoParameters["player_response"]);
            ////Console.WriteLine(encodedStreamsDelimited);
            //JObject jObject = JObject.Parse(encodedStreamsDelimited);
            //string url = null;
            //for (int i = 0; i <= 5; i++)
            //{
            //    try
            //    {
            //        url = (string)jObject["streamingData"]["formats"][i]["url"];
            //    }
            //    catch (Exception error)
            //    {
            //        Console.WriteLine(error);
            //    }
            //    if (url != null) { break; }
            //}
            ////Console.WriteLine("/////////////////////////////////////////// " + url);
            //if (url == null)
            //{
            //    url = await ObtenerStream(videoId);
            //}

            //return url;
            #endregion
        }
        /// <summary>
        /// Obtiene la página a la cual pertenece un elemento
        /// </summary>
        /// <returns>Devuelve la página</returns>
        /// <param name="element">Elemento visual</param>
        public static Page GetParentPage(this VisualElement element)
        {
            if (element != null)
            {
                var parent = element.Parent;
                while (parent != null)
                {
                    if (parent is Page)
                    {
                        return parent as Page;
                    }
                    parent = parent.Parent;
                }
            }
            return null;
        }
    }
}
