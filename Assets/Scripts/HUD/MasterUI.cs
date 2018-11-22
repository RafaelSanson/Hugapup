using API.Model;
using Base;
using UnityEngine;

namespace HUD
{
    [RequireComponent(typeof(Animator))]
    public class MasterUi : MonoBehaviour
    {    
        private Animator _myAnimator;
        public static EventMarker CurrentEventMarker;
        public static MasterUi Instance;
        
        private void Start()
        {
            _myAnimator = GetComponent<Animator>();
            if(Instance != null)
                Destroy(this);
            else
            {
                Instance = this;
            }
        }

        public void StartEventCreator()
        {
            var coordinates = Manager.GetCurrentCoordinates();
            CurrentEventMarker = EventMarker.FromCoordinates(coordinates);
            _myAnimator.SetBool("ShowCreateEvent", true);
        }
        
        public void StartEventDisplay()
        {
            _myAnimator.SetBool("ShowDisplayEvent", true);
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