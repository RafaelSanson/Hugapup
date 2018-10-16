using API.Model;
using Base;
using UnityEngine;

namespace HUD
{
    [RequireComponent(typeof(Animator))]
    public class MasterUI : MonoBehaviour
    {    
        private Animator _myAnimator;
        public static EventMarker CurrentEventMarker;

        private void Start()
        {
            _myAnimator = GetComponent<Animator>();
        }

        public void StartEventCreator()
        {
            var coordinates = Manager.GetCurrentCoordinates();
            CurrentEventMarker = EventMarker.FromCoordinates(coordinates);
            _myAnimator.SetBool("ShowCreateEvent", true);
        }

        public void CancelEventCreator()
        {
            CurrentEventMarker = null;
            EndEventCreator();
        }

        public void ConfirmEventCreator()
        {
            Manager.Instance.CreateMapMarker(CurrentEventMarker);
            EndEventCreator();
        }

        private void EndEventCreator()
        {
            _myAnimator.SetBool("ShowCreateEvent", false);
        }
    }
}