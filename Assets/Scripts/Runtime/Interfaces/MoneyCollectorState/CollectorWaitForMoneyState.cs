using System.Collections;
using Runtime.Enums.NPCState.MoneyCollector;
using Runtime.Extensions;
using Runtime.Managers;
using Runtime.Managers.NPCManager.NPCMoneyCollector;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.MoneyCollectorState
{
    public class CollectorWaitForMoneyState : IStateMachine
    {
        private NPCMoneyCollectorManager Manager;
        private NavMeshAgent Agent;
        private Coroutine WaitMoneyCoroutine;

        public CollectorWaitForMoneyState(NPCMoneyCollectorManager npcMoneyCollectorManager, ref NavMeshAgent navMeshAgent)
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
                WaitMoneyCoroutine = Manager.StartCoroutine(WaitForMoney());
          }

        }


        public IEnumerator WaitForMoney()
        {
            var moneyList = GameSignals.Instance.onGetMoneyStackList?.Invoke();
           
            Debug.Log("Waiting for Money...");
            while (true)
            {
                
                if (moneyList is null || moneyList.Count == 0)
                {
                    Debug.Log("No money found, continuing to wait...");
                    yield return new WaitForSeconds(1f);
                }

                var closestDistance = Mathf.Infinity;
                foreach (var money in moneyList)
                {
                    var distance = Vector3.Distance(Manager.transform.position, money.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        Manager.moneyTransform = money.transform;
                    }
                }

                if (Manager.moneyTransform != null)
                {
                    Manager.OnSwitchState(NPCMoneyCollectorStateType.WalkMoney);
                    yield break;
                }
            }
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
            if(WaitMoneyCoroutine != null) Manager.StopCoroutine(WaitMoneyCoroutine);
        }
    }
}