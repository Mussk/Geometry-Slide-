using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Events
{
    public class LazersEvent : BaseEvent
    {
        [SerializeField] private GameObject lazersPrefab;
        [SerializeField] private int lazersDuration;
        protected override void StartEvent()
        {   
            base.StartEvent();
            lazersPrefab.SetActive(true);
            WaitLazersToDisappear(lazersDuration).Forget();
        }
        
        private async UniTask WaitLazersToDisappear(int duration)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            lazersPrefab.SetActive(false);
        }
    }
}
