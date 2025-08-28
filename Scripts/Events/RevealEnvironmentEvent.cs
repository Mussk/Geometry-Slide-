using UnityEngine;

namespace Events
{
    public class RevealEnvironmentEvent : BaseEvent
    {
        [SerializeField] private GameObject environmentToReveal;
        
        protected override void StartEvent()
        {   
            base.StartEvent();
            environmentToReveal.SetActive(true);
        }

       
    }
}
