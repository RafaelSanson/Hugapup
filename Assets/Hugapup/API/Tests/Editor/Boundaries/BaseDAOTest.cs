using Firebase;
using Firebase.Unity.Editor;
using Hugapup.API.Implementations.Models;
using NUnit.Framework;
using UnityEngine;

namespace Hugapup.API.Tests.Editor.Boundaries
{
    public class BaseDaoTest
    {
        [SetUp]
        public void InitDatabase()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hugapup-firebase.firebaseio.com/");
            //FirebaseApp.DefaultInstance.SetEditorP12FileName("hugapup-8695f00ea8c8.p12");
            //FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("unitytesting@hugapup-firebase.iam.gserviceaccount.com");
            //FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
        }
        
        [Test]
        public async void WhenCreatingAMarkerThenResultMustBeReadable()
        {
            var retrievedMapMarker = default(MapMarker);
            var mapMarker = new MapMarker("myHome", 0.0045, 0.0055, 123123412);

            BaseDao<MapMarker> baseDao = new BaseDaoAdapter<MapMarker>();

            await baseDao.Save(mapMarker, "markers").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Assert.Fail("Save Failed!");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("Save Completed!");
                }
            });

            await baseDao.Retrieve("markers", mapMarker.Title).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                }
                else if (task.IsCompleted)
                {
                    var snapshot = task.Result;
                    var filtereDataSnapshot = snapshot.Child("markers").Child(mapMarker.Title);
                    var json = filtereDataSnapshot.GetRawJsonValue();
                    retrievedMapMarker = (MapMarker) JsonUtility.FromJson(json, typeof(MapMarker));
                }
            });


            Assert.AreEqual(mapMarker, retrievedMapMarker);
        }
    }
}