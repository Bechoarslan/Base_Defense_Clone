using Runtime.Controllers.NpcController.Enemy;
using Runtime.Data.UnityObjects;
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
        private EnemyAnimationController AnimationController;
        public EnemyWalkState(EnemyManager enemyManager, NavMeshAgent navMeshAgent,
            EnemyAnimationController enemyAnimationController)
        {
            Manager = enemyManager;
            Agent = navMeshAgent;
            AnimationController = enemyAnimationController;
            
        }

        public void EnterState()
        {
            AnimationController.OnSetTriggerAnimation(EnemyStateType.Move);
            Manager.Target = GameSignals.Instance.onEnemyWalkPointTransform?.Invoke();
            if (Manager.Target != null) 
                Agent.SetDestination(Manager.Target.position);
        }

        public void UpdateState()
        {
            if (Agent.pathPending) return;

            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
              
            
                Manager.OnEnemyChangeState(EnemyStateType.AttackWall);
            }

        }

        public void OnStateTriggerEnter(Collider other)
        {
            
            if (other.CompareTag(("EnemyChecker")) && other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
            {
                Manager.Target = other.transform.root.gameObject.transform;
         
                Manager.OnEnemyChangeState(EnemyStateType.Run);
            }
        }

        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            
        }
    }
}