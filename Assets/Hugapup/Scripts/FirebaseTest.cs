using System.Collections;
using Firebase.Analytics;
using UnityEngine;

namespace Hugapup.Scripts
{
	public class FirebaseTest : MonoBehaviour {

		// Use this for initialization
		private void Start () {
			FirebaseAnalytics.LogEvent("productInformation", "Nome", Application.productName);
		}
	}
}
