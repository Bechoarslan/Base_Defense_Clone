using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.EnemyState
{ 
   
    public class EnemyWalkState : IStateMachine
    {
        private EnemyManager Manager;
        private NavMeshAgent Agent;
        public EnemyWalkState(EnemyManager enemyManager, NavMeshAgent navMeshAgent)
        {
            Manager = enemyManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
            Manager.WalkPointTransform = GameSignals.Instance.onEnemyWalkPointTransform?.Invoke();
            if (Manager.WalkPointTransform != null) Agent.SetDestination(Manager.WalkPointTransform.position);
        }

        public void UpdateState()
        {
            
            if ( (Manager.transform.position-Manager.WalkPointTransform.transform.position).sqrMagnitude <=Mathf.Pow(Agent.stoppingDistance,2))
            {
               
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}