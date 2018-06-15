using UnityEngine;
using System.Collections;

//This class uses Google Places webservice API. 
//It's made for demo purpose only, and needs your personal Google Developer API Key. 
//(No credit card is required, visit https://developers.google.com/places/web-service/intro)

using GoShared;
using System.Linq;
using MiniJSON;
using System.Collections.Generic;


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
			var url =
				$"{NearbySearchUrl}location={center.latitude},{center.longitude}&radius={radius}&type={Type}&key={GoogleApIkey}&keyword={Keyword}";

			var www = new WWW(url);
			yield return www;

			if (!string.IsNullOrEmpty(www.error)) yield break;
			
			var response = www.text;
			var deserializedResponse = (IDictionary)Json.Deserialize (response);

			var results = (IList)deserializedResponse ["results"];

			foreach (IDictionary result in results) {			
				var placeID = (string)result["place_id"];

				if (_markers.ContainsKey(placeID) && _markers[placeID] != null) continue;

				var location = (IDictionary) ((IDictionary) result["geometry"])["location"];
				var lat = (double) location["lat"];
				var lng = (double) location["lng"];
				var coordinates = new Coordinates(lat, lng, 0);
				
				var place = Instantiate(Prefab);
				var position = coordinates.convertCoordinateToVector(0);

				place.transform.localPosition = position;
				place.transform.SetParent(transform);
				place.name = placeID;

				if (_markers.Contains(placeID))
					_markers[placeID] = place;
				else
					_markers.Add(placeID, place);
			}
			
			
		}
		
	}
}


