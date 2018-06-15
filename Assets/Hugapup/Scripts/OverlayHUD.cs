using Hugapup.API.Tests.Editor.Boundaries;
using UnityEngine;

public class OverlayHUD : MonoBehaviour {


	public void CreateMarker()
	{
		var coordinates = Manager.Instance.GetCurrentCoordinates();
		var marker = MapMarker.FromCoordinates(coordinates);

		Manager.Instance.CreateMapMarker(marker);
	}
}