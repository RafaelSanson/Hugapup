using System;
using System.Collections;
using GoShared;
using UnityEngine;

namespace API.Model
{
    public class EventMarker
    {
        public string Id;
        public string PreviousId;
        public long Timestamp;
        public double Y;
        public double X;
        public int Animal;
        public int Place;
        public int Event;
        public int Owner;
        public int Severity;
        public string Title;

        public static EventMarker FromCoordinates(Coordinates coordinates) => new EventMarker
        {
            Id = string.Empty,
            PreviousId = string.Empty,
            Title = "New event",
            X = coordinates.latitude,
            Y = coordinates.longitude,
            Timestamp = default(long),
            Animal = 0,
            Place = 0,
            Event = 0,
            Owner = 0,
            Severity = 0
        };
        
        public static EventMarker FromDictionary(IDictionary result)
        {
            var eventMarker = new EventMarker();
            
            var idContainer = (IDictionary) result["_id"];
            var id = idContainer["$oid"].ToString();
            if (string.IsNullOrEmpty(id)) return null;
            eventMarker.Id = id;
            
            if (result["x"] == null || result["y"] == null) return null;
            try
            {
                eventMarker.X = double.Parse(result["x"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                eventMarker.Y = double.Parse(result["y"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch(FormatException)
            {
                Debug.Log("Falha ao obter as coordenadas");
                return null;
            }

            try
            {
                eventMarker.Animal = int.Parse(result["animal"].ToString());
                eventMarker.Place = int.Parse(result["place"].ToString());
                eventMarker.Event = int.Parse(result["event"].ToString());
                eventMarker.Owner = int.Parse(result["owner"].ToString());
                eventMarker.Severity = int.Parse(result["severity"].ToString());
            }
            catch (Exception)
            {
                Debug.Log("Falha ao obter os detalhes da ocorrencia");
                return null;
            }

            try
            {
                eventMarker.Timestamp = long.Parse(result["timestamp"].ToString());
            }
            catch (Exception)
            {
                Debug.Log("Falha ao obter o Timestamp da ocorrencia");
                return null;
            }
            
            try
            {
                eventMarker.Title = result["title"].ToString();
                if (string.IsNullOrEmpty(eventMarker.Title))
                    throw new FormatException();
            }
            catch (FormatException)
            {
                Debug.Log("Falha ao obter o título da ocorrência");
                return null;
            }

            try
            {
                eventMarker.PreviousId = result["previousId"].ToString();
            }
            catch (Exception)
            {
                eventMarker.PreviousId = string.Empty;
            }
            
            return eventMarker;
        }
        public string ToJson()
        {
            var json =
                $"('title': '{Title}', 'previousId': '{PreviousId}', 'x': '{X}', 'y': '{Y}', 'timestamp': '{Timestamp}', 'animal': '{Animal}', 'place': '{Place}', 'event': '{Event}', 'owner': '{Owner}', 'severity': '{Severity}')";
            json = json.Replace("(", "{").Replace(")", "}").Replace("'", "\"");
            return json;
        }

        public static EventMarker FromCurrentEvent(Coordinates coordinates, EventMarker currentEventMarker)
        {
            var myMarker = FromCoordinates(coordinates);
            myMarker.Animal = currentEventMarker.Animal;
            myMarker.Place =  currentEventMarker.Place;
            myMarker.Event =  currentEventMarker.Event;
            myMarker.Owner = currentEventMarker.Owner;
            myMarker.Severity = currentEventMarker.Severity;
            myMarker.PreviousId = currentEventMarker.Id;
            return myMarker;
        }
    }
}