using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;

public class FirebaseTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FirebaseAnalytics.LogEvent ("Jogo iniciado!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
