using System;
using API.Model;
using EventHandling;
using HUD;
using UnityEngine;

public class EventMarkerComponent : MonoBehaviour
{
	public Renderer BeamRenderer;
	public Renderer LedRenderer;
	public Renderer AnimalRenderer;
	public MeshFilter MeshFilter;
	private EventMarker _eventMarker;
	public Mesh DogMesh;
	public Mesh CatMesh;
	public Mesh OtherMesh;
	public Material DogMaterial;
	public Material CatMaterial;
	public Material OtherMaterial;

	public Electric ElectricPrefab;
	private Electric _electricChild;

	private void OnMouseDown()
	{
		MasterUI.CurrentEventMarker = _eventMarker;
		MasterUI.Instance.StartEventDisplay();
	}

	private void Update()
	{
		if(_electricChild == null)
			CreateEventBinding();
	}

	public void SetMarker(EventMarker eventMarker)
	{
		_eventMarker = eventMarker;
		
		var color = GetSeverityColor(_eventMarker.Severity);
		color.a = 0.05f;
		BeamRenderer.material.SetColor("_Color", color);
		color.a = 1;
		LedRenderer.materials[1].SetColor("_Color", color);
		
		var mesh = GetAnimalMesh(_eventMarker.Animal);
		MeshFilter.mesh = mesh;

		var material = GetAnimalMaterial(_eventMarker.Animal);
		AnimalRenderer.material = material;
	}

	private void CreateEventBinding()
	{
		if (string.IsNullOrEmpty(_eventMarker.PreviousId)) return;

		var otherMarker = EventMarkerView.Instance.GetEventMarkerComponent(_eventMarker.PreviousId);
		
		if(otherMarker == null) return;

		_electricChild = Instantiate(ElectricPrefab, transform);
		_electricChild.transformPointA = transform;
		_electricChild.transformPointB = otherMarker.transform;
	}

	private Material GetAnimalMaterial(int animal)
	{
		switch (animal)
		{
			case 0:
				return DogMaterial;
			case 1:
				return CatMaterial;
			default:
				return OtherMaterial;
		}
	}

	private Color GetSeverityColor(int severity)
	{
		switch (severity)
		{
			case 0:
				return Color.green;
			case 1:
				return Color.yellow;
			case 2:
				return Color.red;
			default:
				return Color.white;
		}
	}
	
	private Mesh GetAnimalMesh(int animal)
	{
		switch (animal)
		{
			case 0:
				return DogMesh;
			case 1:
				return CatMesh;
			default:
				return OtherMesh;
		}
	}
}
