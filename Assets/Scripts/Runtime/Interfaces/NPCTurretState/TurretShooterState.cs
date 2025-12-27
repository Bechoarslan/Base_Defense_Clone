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
           var turretStandPoint=  GameSignals.Instance.onGetTurretStandPointAndTurretTransform?.Invoke().Item2;
           Manager.transform.SetParent(turretStandPoint);
              Manager.transform.localPosition = Vector3.zero;
                Manager.transform.rotation = Quaternion.identity;
                

        }

        public void UpdateState()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}