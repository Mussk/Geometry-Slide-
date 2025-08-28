using UnityEngine;

namespace Events
{
    public class OneRailEvent : BaseEvent
    {
        
        [SerializeField] private RailManager railManager;
        
        protected override void StartEvent()
        {   
            base.StartEvent();
            railManager.SpawnYOffset = 10;
        }

        protected override void EndEvent()
        {   
            base.EndEvent();
            railManager.SpawnYOffset = 0;
        }
        
    }
}
