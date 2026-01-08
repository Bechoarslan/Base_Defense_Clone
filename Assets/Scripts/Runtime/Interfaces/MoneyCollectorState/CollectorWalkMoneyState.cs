using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums.NPCState.MoneyCollector;
using Runtime.Managers.NPCManager.NPCMoneyCollector;
using Runtime.Signals;
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

            Manager.StartCoroutine(WaitForMoney());
        }

        public IEnumerator WaitForMoney()
        {
            var moneyList = GameSignals.Instance.onGetMoneyStackList?.Invoke();
           
            while (true)
            {
                
                if (moneyList is null || moneyList.Count == 0)
                {
                    
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
                    Agent.SetDestination(Manager.moneyTransform.position);
                    yield return new WaitUntil(() => Agent.remainingDistance <= Agent.stoppingDistance);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            if (other.CompareTag("InOutOfBase"))
            {
               RotateGate(Manager.barrierGate.gameObject,false);
            }
            else if (other.CompareTag("Money"))
            {
                if(Manager.moneyHolder.childCount >= Manager.npcData.Data.MaxStackCount)
                {
                    Debug.Log("Max Capacity Reached");
                    Manager.moneyTransform = null;
                   Manager.OnSwitchState(NPCMoneyCollectorStateType.WaitForMoney);
                }
                Debug.Log("Money Collected");
                GameSignals.Instance.onSendMoneyStackToHolder?.Invoke(Manager.moneyHolder, other.gameObject,Manager.npcData.Data.MaxStackCount);
            }
        }

        private void RotateGate(GameObject blokc, bool _isOpen)
        {
            blokc.transform.DOLocalRotate(
                _isOpen ? new Vector3(0, 0, 0) : new Vector3(0, 0, 90),
                0.4f
            );

           
        }
        
        public void OnStateTriggerExit(Collider other)
        {
            if (other.CompareTag("InOutOfBase"))
            {
                RotateGate(Manager.barrierGate.gameObject,true);
            }
        }

        public void OnExitState()
        {
            Manager.moneyTransform = null;
        }
    }
}