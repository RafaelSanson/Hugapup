using System.Collections;
using System.Text;
using Hugapup.API.Tests.Editor.Boundaries;
using UnityEngine;

public static class Repository
{
    private static string _googlePlacesUrl =
        "https://maps.googleapis.com/maps/api/place/add/json?key=AIzaSyBDtpM_zZ2oiXKDCYqsztR3_GMVcXVd5tk";
	
    public static void CreateMapMarker(MapMarker marker)
    {
        var json = marker.toJson();
        post(_googlePlacesUrl, json);
    }
	
    private static void post(string url, string json)
    {
        var postHeader = new Hashtable {{"Content-Type", "application/json"}};
        var formData = Encoding.UTF8.GetBytes(json);
        new WWW(url, formData, postHeader);
    }
}