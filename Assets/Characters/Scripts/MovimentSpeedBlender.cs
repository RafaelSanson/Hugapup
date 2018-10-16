using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovimentSpeedBlender : MonoBehaviour {

	public float interpolationMultiplier = 0.02f;
	public float maxSpeed = 1f;
	Animator animator;
	Vector3 previousPosition;
	float speed;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		previousPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		float currentSpeed = (currentPosition - previousPosition).magnitude;
		speed = Mathf.Lerp (speed, currentSpeed, interpolationMultiplier);
		float normalizedSpeed = speed / maxSpeed;
		animator.SetFloat ("Speed", normalizedSpeed);
		previousPosition = currentPosition;
	}
}
