using Runtime.Controllers.NpcController.Enemy;
using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.EnemyState
{
    public class EnemyAttackWallState : IStateMachine
    {
        private EnemyManager Manager;
        private EnemyAnimationController AnimationController;
        private NavMeshAgent Agent;
        
        public EnemyAttackWallState(EnemyManager enemyManager, NavMeshAgent navMeshAgent,
            EnemyAnimationController enemyAnimationController)
        {
            Manager = enemyManager;
            Agent = navMeshAgent;
            AnimationController = enemyAnimationController;
            
        }
        public void EnterState()
        {
            Agent.isStopped = true;
            Agent.velocity = Vector3.zero;
            AnimationController.OnSetTriggerAnimation(EnemyStateType.Attack);
        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            if (other.CompareTag(("EnemyChecker")) && other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
            {
                Manager.Target = other.transform.root.gameObject.transform.GetChild(0);
                Debug.Log("Enemy in Attack State to Walk State");
                Manager.OnEnemyChangeState(EnemyStateType.Run);
            }
        }


        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            Agent.isStopped = false;
            
        }
    }
}