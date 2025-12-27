using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IStateMachine
    {
        void EnterState();
        void UpdateState();
        void OnTriggerEnter(Collider other);
        void OnTriggerExit(Collider other);
    }
}