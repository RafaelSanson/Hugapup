using Hugapup.API.Implementations.Models;
using Hugapup.Scripts;
using UnityEngine;

public class OverlayHUD : MonoBehaviour {


	public void CreateMarker()
	{
		var coordinates = Manager.Instance.GetCurrentCoordinates();
		var marker = MapMarker.FromCoordinates(coordinates);
		marker.Title = "Teste de implementação 1";

		Manager.Instance.CreateMapMarker(marker);
	}
}