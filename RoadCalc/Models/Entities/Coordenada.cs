using RoadCalc.Helpers;

namespace RoadCalc.Models.Entities
{
    public class Coordenada : EntityBase
    {
        //Coordenadas Cartesianas
        //Latitude
        public double Lat { get; set; }
        //Longitude
        public double Lng { get; set; }
        
        //UTM
        //Easting
        public double X { get; set; }
        //Northing
        public double Y { get; set; }
        //Elevation
        public double Z { get; set; }
        //Zone Letter
        public string ZoneLetter { get; set; }
        //ZoneNumber
        public int ZoneNumber { get; set; }


        //Construtores
        public Coordenada()
        {
            
        }

        //Quando não são passadas as informações da Zona assume-se Rio de Janeiro
        public Coordenada(double x, double y, double z, bool isUtm)
        {
            LatLngUTMConverter converter;
            if (isUtm)
            {
                X = x;
                Y = y;
                Z = z;
                ZoneLetter = "K";
                ZoneNumber = 23;
                converter = new LatLngUTMConverter("WGS 84");
                var latLng = converter.convertUtmToLatLng(x, y, 23, "K");
                Lat = latLng.Lat;
                Lng = latLng.Lng;
            }
            else
            {
                Lat = x;
                Lng = y;
                converter = new LatLngUTMConverter("WGS 84");
                var utm = converter.convertLatLngToUtm(Lat, Lng);
                ZoneLetter = utm.ZoneLetter;
                ZoneNumber = utm.ZoneNumber;
                X = utm.Easting;
                Y = utm.Northing;
                Z = z;
            }
        }
        //Quando a elevação não é informada assumesse 0
        public Coordenada(double x, double y, bool isUtm)
        {
            LatLngUTMConverter converter;
            if (isUtm)
            {
                X = x;
                Y = y;
                Z = 0;
                ZoneLetter = "K";
                ZoneNumber = 23;
                converter = new LatLngUTMConverter("WGS 84");
                var latLng = converter.convertUtmToLatLng(x, y, 23, "K");
                Lat = latLng.Lat;
                Lng = latLng.Lng;
            }
            else
            {
                Lat = x;
                Lng = y;
                converter = new LatLngUTMConverter("WGS 84");
                var utm = converter.convertLatLngToUtm(Lat, Lng);
                ZoneLetter = utm.ZoneLetter;
                ZoneNumber = utm.ZoneNumber;
                X = utm.Easting;
                Y = utm.Northing;
                Z = 0;
            }
        }


        public Coordenada(double x, double y, double z, int zoneNumber, string zoneLetter)
        {
            X = x;
            Y = y;
            Z = z;
            ZoneLetter = zoneLetter;
            ZoneNumber = zoneNumber;
            var converter = new LatLngUTMConverter("WGS 84");
            var latLng = converter.convertUtmToLatLng(x, y, zoneNumber, zoneLetter);
            Lat = latLng.Lat;
            Lng = latLng.Lng;
        }

        public Coordenada(double x, double y,  int zoneNumber, string zoneLetter)
        {
            X = x;
            Y = y;
            ZoneLetter = zoneLetter;
            ZoneNumber = zoneNumber;
            var converter = new LatLngUTMConverter("WGS 84");
            var latLng = converter.convertUtmToLatLng(x, y, zoneNumber, zoneLetter);
            Lat = latLng.Lat;
            Lng = latLng.Lng;
            Z = ProjetosHelper.GetElevation(this);
        }

        public void RecalculaCoordenadaUTM()
        {
            var converter = new LatLngUTMConverter("WGS 84");
            var latLng = converter.convertUtmToLatLng(X, Y, 23, "K");
            Lat = latLng.Lat;
            Lng = latLng.Lng;
        }
    }
}