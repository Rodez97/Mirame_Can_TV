using MirameCanTV.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MirameCanTV.Clases
{
    public static class VariablesEstaticas
    {
        /// <summary>
        /// Lista de las PlayLists de MírameTV
        /// </summary>
        public static List<PlayListModel> Programas = new List<PlayListModel>
        {
            new PlayListModel{Title="El Programa de MA", Icono=Iconos.VideosCarta, Id="PLzVUXFs4ncHlFSw_aqJ7vclCBQtWnG1dz"},
            new PlayListModel{Title="Canarias Al Día", Icono=Iconos.Palmeras, Id="PLzVUXFs4ncHkghZ_whMPnI6UTLBe_KbZf"},
            new PlayListModel{Title="Noticias Canarias", Icono=Iconos.NoticiasTV, Id="PLzVUXFs4ncHnfudJzFs1AAhkfkZtakaql" },
            new PlayListModel{Title="Mirame Sport", Icono=Iconos.Futbol, Id="PLzVUXFs4ncHmqiYId98xHXQYoJK3143jd" },
            new PlayListModel{Title="Informativos Mírame Sport", Icono=Iconos.Trofeo, Id="PLzVUXFs4ncHn5Mb82Ar9MlC2f4bHZGekp" },
            new PlayListModel{Title="Cocina Conmigo", Icono=Iconos.Comida, Id="PLzVUXFs4ncHmvzNzae9jZ8hhSBIs_FU9R" },
            new PlayListModel{Title="Lucha Canaria", Icono=Iconos.Lucha, Id="PLzVUXFs4ncHmxwJMIBoJii9u6lYhUWoTw" },
            new PlayListModel{Title="Viajar en Canarias", Icono=Iconos.PlayaSol, Id="PLzVUXFs4ncHmoYmk9GpTyKBdyWFWjUomJ" },
            new PlayListModel{Title="Crónica del Campo", Icono=Iconos.Bananas, Id="PLzVUXFs4ncHn69jwKERAvkbOhSEBxqRqB" },
        };
        /// <summary>
        /// ID del canal MírameTV
        /// </summary>
        public const string YoutubeCanalID = "UCjLcsYrJ5tipjjPB30YER-w";
    }
}
