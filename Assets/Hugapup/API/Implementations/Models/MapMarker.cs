using GoShared;

namespace Hugapup.API.Implementations.Models
{
    public class MapMarker
    {
        public long Timestamp;
        public double Y;
        public double X;
        public string Title;

        public MapMarker(string title, double x, double y, long timestamp)
        {
            Title = title;
            X = x;
            Y = y;
            Timestamp = timestamp;
        }

        public static MapMarker FromCoordinates(Coordinates coordinates) => new MapMarker("New marker",
            coordinates.latitude, coordinates.longitude, default(long));

        public string ToJson()
        {
            var json =
                $"('title': '{Title}', 'x': '{X}', 'y': '{Y}', 'timestamp': '{Timestamp}')";
            json = json.Replace("(", "{").Replace(")", "}").Replace("'", "\"");
            return json;
        }
    }
}