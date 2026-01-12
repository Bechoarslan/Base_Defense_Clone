using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.BulletCarrirerState
{
    public class WalkTurretAreaState : IStateMachine
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
            Manager.OnSetTriggerAnim("Run");
            
            Manager.holder = GameSignals.Instance.onGetTurretHolderTransform?.Invoke();
            if (Manager.holder == null) return;
            Agent.isStopped = false;
            Agent.SetDestination(Manager.holder.position);
        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            if (other.CompareTag("DepositArea"))
            {
                Debug.Log("DepositArea Entered");
                Manager.SwitchState(BulletCarrierStateType.WaitDepositBullet);
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