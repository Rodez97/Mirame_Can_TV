using HtmlAgilityPack;
using MirameCanTV.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace MirameCanTV.Clases
{
    public static class ParseFeedContent
    {
        #region Youtube
        #region Variables
        /// <summary>
        /// URL del feed del canal
        /// </summary>
        private const string CanalURL = @"https://www.youtube.com/feeds/videos.xml?channel_id=" + VariablesEstaticas.YoutubeCanalID;
		/// <summary>
		/// URL del feed de la playlist
		/// </summary>
        private const string PlayListURL = @"https://www.youtube.com/feeds/videos.xml?playlist_id=";
		/// <summary>
		/// URL de información del video
		/// </summary>
        private const string VideoURL = @"http://www.youtube.com/get_video_info?video_id=";
        #endregion
        /// <summary>
		/// Esta función obtiene el feed del canal/playlist seleccionado
		/// </summary>
		/// <param name="info_ID">ID de la playlist (Dejar vacio si deseas el feed del canal)</param>
		/// <returns>Devuelve una lista con 15 videos del canal/playlist seleccionado (ItemModel)</returns>
        public static async Task<List<ItemModel>> ParseFeedYoutube(string info_ID)
		{
			string patternFirst = @"([<A-z]+):([A-z>]+)?|([</A-z]+):([A-z>]+)?|([<A-z]+):([A-z]+)?";
			string xrss = string.Empty;
			await Task.Run(async () =>
			{
				xrss = await IntentarCargarContenidoWeb(string.IsNullOrEmpty(info_ID) ? CanalURL : (PlayListURL + info_ID));
			});
			var rss = Regex.Replace(HttpUtility.HtmlDecode(xrss), patternFirst, "$1$2$3$4$5$6");
			var xdoc = XDocument.Parse(rss);
			XNamespace df = xdoc.Root.Name.Namespace;
			return (from item in xdoc.Descendants(df + "entry")
					select new ItemModel
					{
						Title = (string)item.Element(df + "title"),
						Description = (string)item.Element(df + "mediadescription"),
						Url = (string)item.Element(df + "link"),
						UploadDate = (DateTime.Parse((string)item.Element(df + "updated"))).ToString("ddd d", CultureInfo.CreateSpecificCulture("es-ES")) + " de " +
									 (DateTime.Parse((string)item.Element(df + "updated"))).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")),
						ImageUrlHD = "https://i2.ytimg.com/vi/" + (string)item.Element(df + "ytvideoId") + "/hqdefault.jpg",
						Id = (string)item.Element(df + "ytvideoId")
					}).ToList();
		}
		#endregion

		#region Web
		/// <summary>
		/// Obtiene el feed de noticias de la web de Mírame TV
		/// </summary>
		/// <returns>Devuelve una lista con las noticias desde el feed RSS de la web (NoticiaModel)</returns>
		public static async Task<List<NoticiaModel>> ObtenerFeed()
		{
			string feedRSS = string.Empty;
			await Task.Run(async () =>
			{
				feedRSS = await IntentarCargarContenidoWeb(@"https://mirametv.com/en/news/rss");
			});
			return await Task.Run(() =>
			{
				var xdoc = XDocument.Parse(feedRSS);
				var id = 0;
				return (from item in xdoc.Descendants("item")
						select new NoticiaModel
						{
							Title = (string)item.Element("title"),
							Subtitulo = (string)item.Element("description"),
							Url = (string)item.Element("link"),
							UploadDate = (DateTime.Parse((string)item.Element("pubDate"))).ToString("ddd d", CultureInfo.CreateSpecificCulture("es-ES"))+" de "+
										 (DateTime.Parse((string)item.Element("pubDate"))).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")),
							Author = (string)item.Element("author"),
							Id = (id++).ToString()
						}).ToList();
			});
		}
		/// <summary>
		/// Buscamos en la web por más información relacionada con la noticia (Contenido, Imagen, YoutubeID, etc...)
		/// </summary>
		/// <param name="BaseItem">Noticia (NoticiaModel) sobre la cual queremos buscar más información</param>
		/// <returns>Devuelve la noticia "Ampliada" (NoticiaModel)</returns>
		public static async Task<NoticiaModel> DetallesNoticias(NoticiaModel BaseItem)
		{
			string TextoHTML = string.Empty;
			await Task.Run(async () =>
			{
				TextoHTML = await IntentarCargarContenidoWeb(BaseItem.Url);
			});
			HtmlDocument Documento = new HtmlDocument();
			Documento.LoadHtml(TextoHTML);

			#region method one
			var NoticiaNodes = Documento.DocumentNode.SelectSingleNode("//section[@class='col-md-9 col-md-pull-3']/article");
			if (NoticiaNodes == null)
			{
				Console.WriteLine("No se ha encontrado ningún Item");
			}
			else
			{
				if (NoticiaNodes.NodeType == HtmlNodeType.Element)
				{
					//Cargar info
					var BloquedeNoticias = NoticiaNodes.SelectSingleNode(".//div[@class='body f-width']");
					BaseItem.Contenido = HttpUtility.HtmlDecode(BloquedeNoticias.InnerHtml);
					try
                    {
						BaseItem.ImageUrl = @"https://www.mirametv.com" + NoticiaNodes.SelectSingleNode(".//div[@class='aspect-wide']/figure/img").Attributes["src"].Value;
                    }
                    catch (Exception Error)
                    {
                        Console.WriteLine(Error);
						try
						{
							BaseItem.VideoID = (NoticiaNodes.SelectSingleNode(".//div[@class='aspect-wide']/iframe").Attributes["src"].Value).Split('/').Last();
						}
						catch (Exception ErrorInt)
						{
							Console.WriteLine(ErrorInt);
							BaseItem.ImageUrl = "Placeholder.jpg";
						}

                    }
                    return BaseItem;
				}
				else
				{
					Console.WriteLine("Not an element");
				}
			}
			return BaseItem;
			#endregion
		}
		#endregion

		#region Addons
		/// <summary>
		/// Carga el contenido de la URL seleccionada
		/// </summary>
		/// <param name="html">WTF? html???, I mean, la url</param>
		/// <returns>Devuelve la string con el contenido de la URL</returns>
		public static async Task<string> IntentarCargarContenidoWeb(string html)
		{
			int IntentoActual = 0;
			using
				(
					var httpClient = new HttpClient(new HttpClientHandler
					{
						AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
					})
				)
			{
				for (; ; )
				{
					try
					{
						Uri geturi = new Uri(html); //replace your xml url   
						using(var client = new HttpClient())
						{
							HttpResponseMessage responseGet = await client.GetAsync(geturi);
							return await responseGet.Content.ReadAsStringAsync();
						}	
					}
					catch (Exception Error)
					{
						Console.WriteLine(Error);
						IntentoActual++;
						if (IntentoActual >= 3 || !IsTransient(Error))
						{
							throw;
						}
					}
					await Task.Delay(200);
				}
			}
		}
		private static bool IsTransient(Exception ex)
		{
			var webException = ex as WebException;
			if (webException != null)
			{
				// If the web exception contains one of the following status values
				// it might be transient.
				return new[] {WebExceptionStatus.ConnectionClosed,
				  WebExceptionStatus.Timeout,
				  WebExceptionStatus.RequestCanceled }.
						Contains(webException.Status);
			}

			// Additional exception checking logic goes here.
			return false;
		}
		#endregion
	}
}
