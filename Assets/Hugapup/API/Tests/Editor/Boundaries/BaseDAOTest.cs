using Firebase;
using Firebase.Unity.Editor;
using NUnit.Framework;
using UnityEditorInternal;
using UnityEngine;

namespace Hugapup.API.Tests.Editor.Boundaries
{
	[TestFixture]
	public class BaseDaoTest : MonoBehaviour {

		

		[SetUp]
		public void SetDatabaseCredentials()
		{
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hugapup-firebase.firebaseio.com/");
			FirebaseApp.DefaultInstance.SetEditorP12FileName("hugapup-8695f00ea8c8.p12");
			FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("unitytesting@hugapup-firebase.iam.gserviceaccount.com");
			FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
			
			DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
		}

		[Test]
		public void WhenCreatingAMarkerThenResultMustBeReadable()
		{
			var mapMarker = new MapMarker("hame", 0.0045, 0.0055, 123123412);
			IBaseDao<MapMarker> baseDao = new BaseDaoAdapter<MapMarker>();
			baseDao.Save(mapMarker);
			var newMapMarker = baseDao.Retrieve();
			Assert.AreEqual(mapMarker, newMapMarker);

		}
	}

	public interface IBaseDao<T>
	{
		void Save(T saveObject, string parent, string name);
		T Retrieve();
	}

	public class BaseDaoAdapter<T> : IBaseDao<T>
	{

		public void Save(T saveObject, string parent, string name)
		{
			string json = JsonUtility.ToJson(saveObject);

			mDatabaseRef.Child(parent).Child(name).SetRawJsonValueAsync(json);
		}

		public T Retrieve()
		{
			throw new System.NotImplementedException();
		}
	}

	public class MapMarker
	{
		private long timestamp { get; set; }
		private double y { get; set; }
		private double x { get; set; }
		private string name { get; set; }
		
		public MapMarker(string name, double x, double y, long timestamp)
		{
			this.name = name;
			this.x = x;
			this.y = y;
			this.timestamp = timestamp;
		}
	}
}
