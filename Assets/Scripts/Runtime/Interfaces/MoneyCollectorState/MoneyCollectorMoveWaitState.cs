using System.Collections;
using Runtime.Enums;
using Runtime.Enums.NPCState.MoneyCollector;
using Runtime.Extensions;
using Runtime.Managers;
using Runtime.Managers.NPCManager.NPCMoneyCollector;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.MoneyCollectorState
{
    public class MoneyCollectorMoveWaitState : IStateMachine
    {
        private NPCMoneyCollectorManager Manager;
        private NavMeshAgent Agent;
        private Coroutine WaitMoneyCoroutine;
        private bool _isReachedDestination;

        public MoneyCollectorMoveWaitState(NPCMoneyCollectorManager npcMoneyCollectorManager, ref NavMeshAgent navMeshAgent)
        {
            Manager = npcMoneyCollectorManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
          Manager.waitPoint = GameSignals.Instance.onSendNPCMoneyCollectorWalkPoint?.Invoke();
          
          if(Manager.waitPoint !=null)
          {
              Agent.SetDestination(Manager.waitPoint.position);
             
          }

        }


       

        public void UpdateState()
        {

            if (Agent.remainingDistance <= Agent.stoppingDistance && !_isReachedDestination)
            {
                if (Manager.moneyHolder.childCount > 0)
                {
                    PlayerSignals.Instance.onSendStacksToHolder?.Invoke(Manager.moneyHolder,PoolType.Money);
                }
                Manager.OnSwitchState(NPCMoneyCollectorStateType.WalkMoney);
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
            if(WaitMoneyCoroutine != null) Manager.StopCoroutine(WaitMoneyCoroutine);
        }
    }
}