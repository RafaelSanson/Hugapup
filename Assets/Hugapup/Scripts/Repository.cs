using System.Collections;
using System.Text;
using Hugapup.API.Implementations.Models;
using UnityEngine;
#pragma warning disable 618

namespace Hugapup.Scripts
{
    public class Repository : MonoBehaviour
    {
        private static string _stitchUrl =
            "https://webhooks.mongodb-stitch.com/api/client/v2.0/app/hugapupbackend-nrlaq/service/HugapupBackendHTTP/incoming_webhook/createEvent";

        public static Repository Instance;


        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }
        }
	
        public void CreateMapMarker(MapMarker marker)
        {
            var json = marker.ToJson();
            StartCoroutine(Post(_stitchUrl, json));
        }
	
        private static IEnumerator Post(string url, string json)
        {
            var postHeader = new Hashtable {{"Content-Type", "application/json"}};
            var formData = Encoding.UTF8.GetBytes(json);
            using (var www = new WWW(url, formData, postHeader))
            {
                yield return www;
                var result = Encoding.UTF8.GetString(www.bytes);
                Debug.Log(result);
            }
        }
    }
}
