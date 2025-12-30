using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.BulletCarrierState
{
    public class WalkAmmoAreaState : IStateMachine
    {
        private NPCBulletCarrierManager Manager;
        private NavMeshAgent Agent;

        public WalkAmmoAreaState(NPCBulletCarrierManager npcBulletCarrierManager, ref NavMeshAgent navMeshAgent)
        {
            Manager = npcBulletCarrierManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
            var ammoTransform = GameSignals.Instance.onGetStackAmmoHolderTransform?.Invoke();
            if (ammoTransform != null) 
                Agent.SetDestination(ammoTransform.position);
        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
                Debug.Log("Ammo Area Entered");
                Agent.isStopped = true;
                Manager.SwitchState(BulletCarrierStateType.WaitTakeBullet);
            }
        }

        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            
        }
    }
}