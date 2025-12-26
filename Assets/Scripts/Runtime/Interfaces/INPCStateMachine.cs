using UnityEngine;

namespace Runtime.Interfaces
{
    public interface INPCStateMachine
    {
        void EnterState();
        void UpdateState();
        void OnTriggerEnter(Collider other);
        void OnTriggerExit(Collider other);
    }
}