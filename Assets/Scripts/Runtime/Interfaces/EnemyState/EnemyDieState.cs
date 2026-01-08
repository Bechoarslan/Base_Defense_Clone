using Runtime.Controllers.NpcController.Enemy;
using Runtime.Enums;
using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.EnemyState
{
    public class EnemyDieState : IStateMachine
    {
        private EnemyManager Manager;
        private NavMeshAgent Agent;
        private EnemyAnimationController _enemyAnimationController;
        
        public EnemyDieState(EnemyManager enemyManager, NavMeshAgent navMeshAgent,
            EnemyAnimationController enemyAnimationController)
        {
            Manager = enemyManager;
            Agent = navMeshAgent;
            _enemyAnimationController = enemyAnimationController;
        }

        public void EnterState()
        {
           Agent.isStopped = true;
           for (int i = 0; i < 3; i++)
           {
               
               var money = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Money);
               GameSignals.Instance.onAddListDroppedMoneyFromEnemy?.Invoke(money);
                var newPos = Manager.transform.position + new Vector3(Random.Range(-1f,1f),1,
                     Random.Range(-1f,1f));
                newPos.y = 0;
                money.transform.position = newPos;
                money.SetActive(true);
                money.transform.SetParent(null);
                
                
           }
           _enemyAnimationController.OnSetBoolAnimation(true);
           _enemyAnimationController.OnSetTriggerAnimation(EnemyStateType.Die);
           Manager.Target = null;
         
           
           
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