using System.Collections;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.HostageState
{
    public class MoveToMineState : IStateMachine
    {
        private NPCHostageManager Manager;
        private NavMeshAgent Agent;
        public MoveToMineState(NPCHostageManager manager, ref NavMeshAgent navMeshAgent)
        {
            Manager = manager;
            Agent = navMeshAgent;
        }
        public void EnterState()
        {
            Debug.Log("Entering MoveToMineState");
            var miningAreaTransform = GameSignals.Instance.onGetMiningAreaTransform.Invoke();
            Agent.SetDestination(miningAreaTransform.position);
        }
        

        public void UpdateState()
        {
            if(Agent.pathPending) return;
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                Debug.Log("Reached Mining Area");
            }
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