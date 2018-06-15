using GoShared;
using Hugapup.API.Tests.Editor.Boundaries;
using UnityEngine;

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
        Repository.CreateMapMarker(marker);
    }
}