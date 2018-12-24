using System;
using HUD;
using UnityEngine;
using UnityEngine.UI;

public enum DropdownType { Animal, Place, Event, Owner, Severity}

public class CreateEventHandler : MonoBehaviour
{
	public Dropdown DropdownAnimal, DropdownPlace, DropdownEvent, DropdownOwner, DropdownSeverity;
	public static CreateEventHandler Instance;

	public void Start()
	{
		if(Instance != null)
			Destroy(this);
		else
		{
			Instance = this;
		}
	}
	
	
	
	public void OnAnimalValueChanged(Dropdown dropdown) => OnValueChanged(dropdown, DropdownType.Animal);
	public void OnPlaceValueChanged(Dropdown dropdown) => OnValueChanged(dropdown, DropdownType.Place);
	public void OnEventValueChanged(Dropdown dropdown) => OnValueChanged(dropdown, DropdownType.Event);
	public void OnOwnerValueChanged(Dropdown dropdown) => OnValueChanged(dropdown, DropdownType.Owner);
	public void OnSeverityValueChanged(Dropdown dropdown) => OnValueChanged(dropdown, DropdownType.Severity);
	
	private void OnValueChanged(Dropdown dropdown, DropdownType dropdownType)
	{
		var currentEventMarker = MasterUI.CurrentEventMarker;
		if (currentEventMarker == null) return;

		switch (dropdownType)
		{
			case DropdownType.Animal:
				currentEventMarker.Animal = dropdown.value;
				break;
			case DropdownType.Place:
				currentEventMarker.Place = dropdown.value;
				break;
			case DropdownType.Event:
				currentEventMarker.Event = dropdown.value;
				break;
			case DropdownType.Owner:
				currentEventMarker.Owner = dropdown.value;
				break;
			case DropdownType.Severity:
				currentEventMarker.Severity = dropdown.value;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(dropdownType), dropdownType, null);
		}
	}

	public void SetPreviousValue()
	{
		if (MasterUI.CurrentEventMarker == null) return;
		
		DropdownAnimal.value = MasterUI.CurrentEventMarker.Animal;
		DropdownPlace.value =  MasterUI.CurrentEventMarker.Place;
		DropdownEvent.value =  MasterUI.CurrentEventMarker.Event;
		DropdownOwner.value =  MasterUI.CurrentEventMarker.Owner;
		DropdownSeverity.value =  MasterUI.CurrentEventMarker.Severity;
	}
}
