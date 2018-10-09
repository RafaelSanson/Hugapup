using System;
using UnityEngine;
using System.Collections;

//This class uses Google Places webservice API. 
//It's made for demo purpose only, and needs your personal Google Developer API Key. 
//(No credit card is required, visit https://developers.google.com/places/web-service/intro)

using GoShared;
using System.Linq;
using MiniJSON;
using System.Collections.Generic;
using Hugapup.Scripts;


namespace GoMap
{

	public class GOPlaces : MonoBehaviour {

		public GOMap GoMap;
		public string GoogleApIkey;
		public string Type;
		public string Keyword;
		public GameObject Prefab;

		private Hashtable _markers;

		private const string NearbySearchUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";
		[HideInInspector] public IDictionary IconsCache = new Dictionary <string,Sprite>();

		private void Start () {
			_markers = new Hashtable();
			StartCoroutine(RefreshTiles());
		}

		private IEnumerator RefreshTiles()
		{
			while (true)
			{
				var coords = Manager.Instance.GetCurrentCoordinates();
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

			foreach (IDictionary result in results) {			
				var idContainer = (IDictionary) result["_id"];
				var id = idContainer["$oid"].ToString();
				var title = (string) result["title"];
				
				if (_markers.ContainsKey(id) && _markers[id] != null) continue;
				if (result["x"] == null || result["y"] == null) continue;
				
				var lat = double.Parse(result["x"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
				var lng = double.Parse(result["y"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
				var coordinates = new Coordinates(lat, lng, 0);
				
				var place = Instantiate(Prefab);
				var position = coordinates.convertCoordinateToVector(0);

				place.transform.localPosition = position;
				place.transform.SetParent(transform);
				place.name = title;

				if (_markers.Contains(id))
					_markers[id] = place;
				else
					_markers.Add(id, place);
					Debug.Log($"O evento [{place.name}] foi adicionado ao mapa!");
			}
			
			
		}
		
	}
}


