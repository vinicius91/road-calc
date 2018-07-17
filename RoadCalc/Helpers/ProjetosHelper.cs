using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoadCalc.Models.Entities;

namespace RoadCalc.Helpers
{
    public class ProjetosHelper
    {
        private static NumberFormatInfo _numberFormat;

        public ProjetosHelper()
        {
            _numberFormat = new NumberFormatInfo();
            _numberFormat.NumberDecimalSeparator = ".";
            _numberFormat.NumberDecimalDigits = 10;
        }
        public static async Task<double> GetElevationAsync(Coordenada coordenada)
        {
            var numberFormat = new NumberFormatInfo();
            numberFormat.NumberDecimalSeparator = ".";
            numberFormat.NumberDecimalDigits = 10;
            //https://developers.google.com/maps/documentation/elevation/intro
            var request = (HttpWebRequest)WebRequest.Create(string.Format("https://maps.googleapis.com/maps/api/elevation/json?locations={0},{1}&key={2}", coordenada.Lat.ToString(numberFormat), coordenada.Lng.ToString(numberFormat), "AIzaSyAmrR9PRaet7MP0lWudVRE48JSDJXICF3s"));
            var response = (HttpWebResponse) await request.GetResponseAsync();
            var sr = await new StreamReader(response.GetResponseStream() ?? new MemoryStream()).ReadToEndAsync();

            var json = JObject.Parse(sr);
            return (double)json.SelectToken("results[0].elevation");
        }

        public static double GetElevation(Coordenada coordenada)
        {
            var numberFormat = new NumberFormatInfo();
            numberFormat.NumberDecimalSeparator = ".";
            numberFormat.NumberDecimalDigits = 10;
            //https://developers.google.com/maps/documentation/elevation/intro
            var request = (HttpWebRequest)WebRequest.Create(string.Format("https://maps.googleapis.com/maps/api/elevation/json?locations={0},{1}&key={2}", coordenada.Lat.ToString(numberFormat), coordenada.Lng.ToString(numberFormat), "AIzaSyAmrR9PRaet7MP0lWudVRE48JSDJXICF3s"));
            var response = (HttpWebResponse)request.GetResponse();
            var sr = new StreamReader(response.GetResponseStream() ?? new MemoryStream()).ReadToEnd();

            var json = JObject.Parse(sr);
            return (double)json.SelectToken("results[0].elevation");
        }

        public static IEnumerable<Coordenada> GetElevationsPath(string encodedPath, int samples)
        {
            //https://developers.google.com/maps/documentation/elevation/intro
            var request = (HttpWebRequest)WebRequest.Create(string.Format("https://maps.googleapis.com/maps/api/elevation/json?path=enc:{0}&samples={1}&key={2}", encodedPath, samples, "AIzaSyAmrR9PRaet7MP0lWudVRE48JSDJXICF3s"));
            var response = (HttpWebResponse)request.GetResponse();
            var sr = new StreamReader(response.GetResponseStream() ?? new MemoryStream()).ReadToEnd();
            var rootObj = JsonConvert.DeserializeObject<RootObject>(sr);
            var coordenadas = new List<Coordenada>();
            foreach (var obj in rootObj.results)
            {
                coordenadas.Add(new Coordenada(obj.location.lat, obj.location.lng, obj.elevation, false));
            }

            return coordenadas;
        }

        public static IEnumerable<Estaca> CalculaEstacaRelatorio()
        {


            return new List<Estaca>();
        }
    }
}