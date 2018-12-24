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
        public static MasterUI Instance;
        
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

        public void CloseWindows()
        {
            CurrentEventMarker = null;
            _myAnimator.SetBool("ShowCreateEvent", false);
            _myAnimator.SetBool("ShowDisplayEvent", false);
        }

        public void ConfirmEventCreator()
        {
            Manager.Instance.CreateMapMarker(CurrentEventMarker);
            CloseWindows();
        }

        public void UpdateEvent()
        {
            var coordinates = Manager.GetCurrentCoordinates();
            CurrentEventMarker = EventMarker.FromCurrentEvent(coordinates, CurrentEventMarker);
            
            _myAnimator.SetBool("ShowCreateEvent", true);
            _myAnimator.SetBool("ShowDisplayEvent", false);

            CreateEventHandler.Instance.SetPreviousValue();
        }
    }
}