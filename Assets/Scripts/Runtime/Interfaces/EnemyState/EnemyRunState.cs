using Runtime.Managers.EnemyManager;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.EnemyState
{
    public class EnemyRunState : IStateMachine
    {
        private EnemyManager Manager;
        private NavMeshAgent Agent;
        public EnemyRunState(EnemyManager enemyManager, NavMeshAgent navMeshAgent)
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