using RunTime.Enums.NPC;
using RunTime.Enums.Pool;
using RunTime.Interfaces;
using RunTime.Managers;
using RunTime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace RunTime.State
{
    public class EnemyWalkState : INPCStates
    {
        private NavMeshAgent _navMeshAgent;
        private EnemyManager _enemyManager;
        public EnemyWalkState(NavMeshAgent navMeshAgent, EnemyManager enemyManager)
        {
            _navMeshAgent = navMeshAgent;
            _enemyManager = enemyManager;
        }

        public void PerformAction()
        {
            var pos = NavMeshSignals.Instance.onSendEnemyWalkArea?.Invoke();
            _navMeshAgent.SetDestination(pos.transform.position);
        }

        public void ChangeAnimation()
        {
            throw new System.NotImplementedException();
        }

        public void SetAnimationState(NPCAnimationState animationState)
        {
            throw new System.NotImplementedException();
        }

       
    }
}