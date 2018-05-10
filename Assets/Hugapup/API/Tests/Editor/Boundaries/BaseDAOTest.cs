using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using NUnit.Framework;
using UnityEngine;

namespace Hugapup.API.Tests.Editor.Boundaries
{
	public class BaseDaoTest
	{
		[Test]
		public async void WhenCreatingAMarkerThenResultMustBeReadable()
		{			
			var retrievedMapMarker = default(MapMarker);
			var mapMarker = new MapMarker("myHome", 0.0045, 0.0055, 123123412);
			IBaseDao<MapMarker> baseDao = new BaseDaoAdapter<MapMarker>();
	
			await baseDao.Save(mapMarker, "mapMarker", mapMarker.Name).ContinueWith(task => {
				if (task.IsFaulted) {
					Debug.Log("Save Failed!");
				}
				else if (task.IsCompleted) {
					Debug.Log("Save Completed!");
				}
			});
			
			await baseDao.Retrieve("mapMarker", mapMarker.Name).ContinueWith(task => {
				if (task.IsFaulted) {
				}
				else if (task.IsCompleted) {
					var snapshot = task.Result;
					var filtereDataSnapshot = snapshot.Child("mapMarker").Child(mapMarker.Name);
					var json = filtereDataSnapshot.GetRawJsonValue();
					retrievedMapMarker = (MapMarker) JsonUtility.FromJson(json, typeof(MapMarker));
				}
			});
			Assert.AreEqual(mapMarker, retrievedMapMarker);
		}
	}

	public interface IBaseDao<in T>
	{
		Task Save(T saveObject, string parent, string name);
		Task<DataSnapshot> Retrieve(string parent, string name);
	}

	public class BaseDaoAdapter<T> : IBaseDao<T>
	{
		private readonly DatabaseReference _databaseReference;
		
		public BaseDaoAdapter()
		{
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hugapup-firebase.firebaseio.com/");
			//FirebaseApp.DefaultInstance.SetEditorP12FileName("hugapup-8695f00ea8c8.p12");
			//FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("unitytesting@hugapup-firebase.iam.gserviceaccount.com");
			//FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
			
			_databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
		}
		
		public Task Save(T saveObject, string parent, string name)
		{
			var json = JsonUtility.ToJson(saveObject);

			 return _databaseReference.Child(parent).Child(name).SetRawJsonValueAsync(json);
		}

		public Task<DataSnapshot> Retrieve(string parent, string name)
		{
			var firebaseDatabase = FirebaseDatabase.DefaultInstance;
			var databaseReference = firebaseDatabase.GetReference(parent);
			var query = databaseReference.OrderByChild(name).LimitToFirst(1);
			return query.GetValueAsync();
		}
	}

	public class MapMarker
	{
		private long Timestamp { get; set; }
		private double Y { get; set; }
		private double X { get; set; }
		public string Name { get; set; }
		
		public MapMarker(string name, double x, double y, long timestamp)
		{
			this.Name = name;
			this.X = x;
			this.Y = y;
			this.Timestamp = timestamp;
		}
	}
}
