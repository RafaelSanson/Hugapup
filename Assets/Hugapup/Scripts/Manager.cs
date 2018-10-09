using GoShared;
using Hugapup.API.Implementations.Models;
using UnityEngine;

namespace Hugapup.Scripts
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;
	
        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }
        }

        public Coordinates GetCurrentCoordinates()
        {
            return GameObject.Find("LocationManager").GetComponent<LocationManager>().currentLocation;
        }

        public void CreateMapMarker(MapMarker marker)
        {
            if(Repository.Instance != null)
                Repository.Instance.CreateMapMarker(marker);
        }
    }
}