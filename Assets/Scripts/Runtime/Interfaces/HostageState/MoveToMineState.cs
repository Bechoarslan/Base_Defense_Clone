using System.Collections;
using Runtime.Enums;
using Runtime.Enums.NPCState;
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
             var miningAreaTransform = GameSignals.Instance.onGetMiningAreaTransform.Invoke();
             
             Manager.gemMineType = miningAreaTransform.Item2;
             if (Manager.gemMineType == GemMineType.CartMine)
             {
                 GameSignals.Instance.onHostageIsCartMining?.Invoke(true);
             }
             Manager.mineAreaTransform = miningAreaTransform.Item1;
            Agent.SetDestination(miningAreaTransform.Item1.position);
        }
        

        public void UpdateState()
        {
            if(Agent.pathPending) return;
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                
               Manager.SwitchState(NPCHostageStateType.MineAndCarryGem);
              
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