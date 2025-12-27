using Runtime.Managers.EnemyManager;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.EnemyState
{
    public class EnemyDieState : IStateMachine
    {
        private EnemyManager Manager;
        private NavMeshAgent Agent;
        public EnemyDieState(EnemyManager enemyManager, NavMeshAgent navMeshAgent)
        {
            Manager = enemyManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
           
        }

        public void UpdateState()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
           
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}