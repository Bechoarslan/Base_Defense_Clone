using Runtime.Managers.NPCManager.NPCMoneyCollector;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.MoneyCollectorState
{
    public class CollectorWalkMoneyState : IStateMachine
    {
        private NPCMoneyCollectorManager Manager;
        private NavMeshAgent Agent;

        public CollectorWalkMoneyState(NPCMoneyCollectorManager npcMoneyCollectorManager, ref NavMeshAgent navMeshAgent)
        {
            Manager = npcMoneyCollectorManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
            Agent.destination = Manager.moneyTransform.position;
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