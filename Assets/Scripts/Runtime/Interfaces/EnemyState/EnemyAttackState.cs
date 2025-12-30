
    using Runtime.Controllers.NpcController.Enemy;
    using Runtime.Enums.EnemyStateType;
    using Runtime.Interfaces;
    using Runtime.Managers.EnemyManager;
    using UnityEngine;
    using UnityEngine.AI;

    public class EnemyAttackState : IStateMachine
    {
        private EnemyManager Manager;

        private NavMeshAgent Agent;

        private EnemyAnimationController AnimationController;

        private bool _isAttacking;

        public EnemyAttackState(EnemyManager enemyManager, NavMeshAgent navMeshAgent,

            EnemyAnimationController enemyAnimationController)

        {

            Manager = enemyManager;

            Agent = navMeshAgent;

            AnimationController = enemyAnimationController;

        }



        public void EnterState()

        {
            
            Agent.isStopped = true;
            
        }






        public void UpdateState()

        {

            Agent.destination = Manager.Target.transform.position;

            if ((Manager.transform.position-Manager.Target.transform.position).sqrMagnitude > Mathf.Pow(Agent.stoppingDistance, 2))

            {

                Manager.OnEnemyChangeState(EnemyStateType.Run);

            }

        }




        public void OnStateTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Player"))

            {

                Debug.Log("Attacked Player");
                AnimationController.OnSetTriggerAnimation(EnemyStateType.Attack);

            }


        }



        public void OnStateTriggerExit(Collider other)

        {

            if (other.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Player"))

            {

            

            }

        }

        public void OnExitState()
        {
            
        }
    }

