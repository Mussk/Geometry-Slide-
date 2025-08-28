using UnityEngine;

namespace Events
{
    public abstract class BaseEvent : MonoBehaviour
    {
        [SerializeField] protected int EventRepeatEachXPoints;
        [SerializeField] protected int EventDurationPoints;
        [SerializeField] protected bool HasEnd;
        protected int CurrentEventCounts = 1;
        protected bool IsEventStarted = false;

        private void OnEnable()
        {
            ScoreManager.ScoreChangedEvent += CheckIfStartEvent;
        }

        private void OnDisable()
        {
            ScoreManager.ScoreChangedEvent -= CheckIfStartEvent;
        }


        protected void CheckIfStartEvent(int currentScore)
        {
            int eventStartPoints = EventRepeatEachXPoints * CurrentEventCounts;

            if (currentScore >= eventStartPoints
                && !IsEventStarted)
            {
                IsEventStarted = true;
                Debug.Log("Event Started");
                StartEvent();
                
            
            }
            if (!HasEnd) return;
            if (currentScore <= eventStartPoints + EventDurationPoints) return;
            IsEventStarted = false;
            Debug.Log("Event Ended");
            EndEvent();
            CurrentEventCounts += 1;

        }
    
        protected virtual void StartEvent()
        {
        
        }

        protected virtual void EndEvent()
        {
        
        }

    }
}
