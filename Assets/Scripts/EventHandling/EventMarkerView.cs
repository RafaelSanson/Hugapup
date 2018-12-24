using System.Collections;
using System.Collections.Generic;
using API.Model;
using Base;
using GoMap;
using GoShared;
using Hugapup.Scripts;
using MiniJSON;
using UnityEngine;

namespace EventHandling
{

    public class EventMarkerView : MonoBehaviour
    {

        public static EventMarkerView Instance;
        
        public EventMarkerComponent EventPrefab;

        private Hashtable _markers;


     
        
        [HideInInspector] public IDictionary IconsCache = new Dictionary <string,Sprite>();

        private void Start ()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;
            
            _markers = new Hashtable();
            StartCoroutine(RefreshTiles());
        }

        private IEnumerator RefreshTiles()
        {
            while (true)
            {
                var coords = Manager.GetCurrentCoordinates();
                StartCoroutine(NearbySearch(coords, 200f));
                yield return new WaitForSecondsRealtime(10f);
            }
        }
		

        private IEnumerator NearbySearch (Coordinates center, float radius) {
            const string url = "https://webhooks.mongodb-stitch.com/api/client/v2.0/app/hugapupbackend-nrlaq/service/HugapupBackendHTTP/incoming_webhook/getEvents";       

            var www = new WWW(url);
            yield return www;
            
            if (!string.IsNullOrEmpty(www.error)) yield break;
			
            var response = www.text;
            var results = (IList)Json.Deserialize (response);
            
            foreach (IDictionary result in results)
            {
                var eventMarker = EventMarker.FromDictionary(result);
                if (eventMarker == null) continue;
                
                if (!_markers.ContainsKey(eventMarker.Id) || _markers[eventMarker.Id] == null)
                {
                    var coordinates = new Coordinates(eventMarker.X, eventMarker.Y, 0);

                    var eventMarkerPrefab = Instantiate(EventPrefab);
                    var position = coordinates.convertCoordinateToVector(0);

                    eventMarkerPrefab.transform.localPosition = position;
                    eventMarkerPrefab.transform.SetParent(transform);
                    eventMarkerPrefab.SetMarker(eventMarker);

                    _markers.Add(eventMarker.Id, eventMarkerPrefab);
                }
            }
        }

        public EventMarkerComponent GetEventMarkerComponent(string id)
        {
            if(_markers.ContainsKey(id))
                return _markers[id] as EventMarkerComponent;
            return null;
        }
		
    }
}