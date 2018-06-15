using GoShared;
using UnityEngine;

namespace Hugapup.API.Tests.Editor.Boundaries
{
    public class MapMarker
    {
        private long Timestamp { get; set; }
        private double Y { get; set; }
        private double X { get; set; }
        public string Name { get; set; }

        public MapMarker(string name, double x, double y, long timestamp)
        {
            Name = name;
            X = x;
            Y = y;
            Timestamp = timestamp;
        }

        public static MapMarker FromCoordinates(Coordinates coordinates) => new MapMarker("New marker",
            coordinates.latitude, coordinates.longitude, default(long));

        public string toJson()
        {
            var json =
                $"('location': ('lat': {X},'lng': {Y}), 'accuracy': 50, 'name': 'hugapup', 'types': ['other'])";
            json = json.Replace("(", "{").Replace(")", "}").Replace("'", "\"");
            return json;
        }
    }
}