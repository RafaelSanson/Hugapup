using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Unity.Editor;
using NUnit.Framework;
using UnityEngine;

public class BaseDAOTest : MonoBehaviour {

	[Test]
	public async void WhenAuthenticatingWithUnexistingCredentialsThenUserWillBeNull()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hugapup-firebase.firebaseio.com/");
		FirebaseApp.DefaultInstance.SetEditorP12FileName("hugapup-8695f00ea8c8.p12");
		FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("unitytesting@hugapup-firebase.iam.gserviceaccount.com");
		FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
	}
}
