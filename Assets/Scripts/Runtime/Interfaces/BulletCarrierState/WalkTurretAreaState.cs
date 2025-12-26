using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.BulletCarrirerState
{
    public class WalkTurretAreaState : INPCStateMachine
    {
        private NPCBulletCarrierManager Manager;
        private NavMeshAgent Agent;
        public WalkTurretAreaState(NPCBulletCarrierManager npcBulletCarrierManager, ref NavMeshAgent navMeshAgent)
        {
            Manager = npcBulletCarrierManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
            var turretTransform = GameSignals.Instance.onGetTurretHolderTransform?.Invoke();
            if (turretTransform == null) return;
            Agent.isStopped = false;
            Agent.SetDestination(turretTransform.position);
        }

        public void UpdateState()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DepositArea"))
            {
                Debug.Log("DepositArea Entered");
                Manager.SwitchState(BulletCarrierType.WaitDepositBullet);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}