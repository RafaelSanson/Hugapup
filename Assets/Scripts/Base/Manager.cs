using API.Model;
using GoShared;
using Hugapup.Scripts;
using UnityEngine;

namespace Base
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;
        private static LocationManager _locationManager;
	
        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }

            _locationManager = GameObject.Find("LocationManager").GetComponent<LocationManager>();
        }

        public static Coordinates GetCurrentCoordinates() => _locationManager == null ? default(Coordinates) : _locationManager.currentLocation;

        public void CreateMapMarker(EventMarker marker)
        {
            if(Repository.Instance != null)
                Repository.Instance.CreateMapMarker(marker);
        }
    }
}