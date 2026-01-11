using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Interfaces.NPCTurretState
{
    public class TurretShooterState : IStateMachine
    { 
        private NPCTurretManager Manager;
        public TurretShooterState(NPCTurretManager npcTurretManager)
        {
            Manager = npcTurretManager;
        }

        public void EnterState()
        {
           

        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            
        }

        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            
        }
    }
}