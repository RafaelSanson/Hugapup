using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

namespace Hugapup.API.Tests.Editor.Boundaries
{
    public class BaseDaoAdapter<T> : BaseDao<T>
    {
        private readonly DatabaseReference _databaseReference;
		
        public BaseDaoAdapter()
        {
            _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        }
		
        public Task Save(T saveObject, string parent)
        {
            var json = JsonUtility.ToJson(saveObject);

            var key = _databaseReference.Child(parent).Push().Key;
            return _databaseReference.Child(parent).Child(key).SetRawJsonValueAsync(json);
        }

        public Task<DataSnapshot> Retrieve(string parent, string key)
        {
            var firebaseDatabase = FirebaseDatabase.DefaultInstance;
            var databaseReference = firebaseDatabase.GetReference(parent);
            var query = databaseReference.EqualTo(key);
            return query.GetValueAsync();
        }
    }
}