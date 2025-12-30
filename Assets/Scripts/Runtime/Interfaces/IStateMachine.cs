using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IStateMachine
    {
        void EnterState();
        void UpdateState();
        void OnStateTriggerEnter(Collider other);
        void OnStateTriggerExit(Collider other);
        
        void OnExitState();
    }
}