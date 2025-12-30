using System.Collections;
using Runtime.Controllers.NpcController.Enemy;
using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.EnemyState
{
    public class EnemyRunState : IStateMachine
    {
        private EnemyManager Manager;
        private NavMeshAgent Agent;
        private EnemyAnimationController AnimationController;
        private Coroutine _attackCoroutine;
        public EnemyRunState(EnemyManager enemyManager, NavMeshAgent
                navMeshAgent,
            EnemyAnimationController enemyAnimationController)
        {
            Manager = enemyManager;
            Agent = navMeshAgent;
            AnimationController = enemyAnimationController;
            
        }

        public void EnterState()
        {
            Debug.Log(("Entered Run State"));
           Agent.isStopped = false;
           AnimationController.OnSetTriggerAnimation(EnemyStateType.Run);
        }

        public void UpdateState()
        {
            if (Manager.Target is null) return;
            Agent.destination = Manager.Target.transform.position;
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
           if(other.gameObject.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Player"))
           {
               
               Manager.StartCoroutine(DamageToPlayer(other.gameObject.transform.root.gameObject));
               Debug.Log("Attacked to Player");
           }
        }

        private IEnumerator DamageToPlayer(GameObject otherGameObject)
        {
       
            while (true)
            {
              
                AnimationController.OnSetTriggerAnimation(EnemyStateType.Attack);
                yield return new WaitForSeconds(0.6f);
                AnimationController.OnSetTriggerAnimation(EnemyStateType.Run);
                var idamageable = otherGameObject.GetComponent<IDamageable>();
                if (idamageable != null)
                    idamageable.TakeDamage(20);
                yield return new WaitForSeconds(1.5f);
                
            }
  
            
        }


        public void OnStateTriggerExit(Collider other)
        {
           AnimationController.OnSetTriggerAnimation(EnemyStateType.Run);
           if(_attackCoroutine != null)  Manager.StopCoroutine(_attackCoroutine);
           

        }

        public void OnExitState()
        {
            
        }
    }
}