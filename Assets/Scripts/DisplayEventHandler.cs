
using Base;
using HUD;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEventHandler : MonoBehaviour
{	
	public Dropdown DropdownAnimal, DropdownPlace, DropdownEvent, DropdownOwner, DropdownSeverity;
	
	public void Update()
	{
		if (MasterUI.CurrentEventMarker == null) return;
		
		DropdownAnimal.value = MasterUI.CurrentEventMarker.Animal;
		DropdownPlace.value =  MasterUI.CurrentEventMarker.Place;
		DropdownEvent.value =  MasterUI.CurrentEventMarker.Event;
		DropdownOwner.value =  MasterUI.CurrentEventMarker.Owner;
		DropdownSeverity.value =  MasterUI.CurrentEventMarker.Severity;
	}
	
	
}
