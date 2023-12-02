using System.Collections;
using DG.Tweening;
using RunTime.Enums.NPC;
using RunTime.Interfaces;
using RunTime.Managers;
using RunTime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace RunTime.State.Enemy
{
    public class EnemyStartState : INpcStates
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private NPCManager _npcManager;
       
        public EnemyStartState(ref NavMeshAgent navMeshAgent, ref Animator animator, NPCManager npcManager)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _npcManager = npcManager;
        }

        public void StartAction()
        {
            SetAnimation(NPCAnimationEnum.Walk);
            var wallArea = NavMeshSignals.Instance.onSendEnemyWalkArea?.Invoke();
            _npcManager.transform.LookAt(wallArea);
            _navMeshAgent.SetDestination(wallArea.position);
        }

        public void UpdateAction()
        {
            
        }

        public void OnColliderEntered(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _npcManager.ChangeCurrentState(NPCStateType.EnemyRun);
              
            }
            
        }

        public void SetAnimation(NPCAnimationEnum animationEnums)
        {
            _animator.SetTrigger(animationEnums.ToString());
        }

        public void OnColliderEntered()
        {
            
        }

       
    }
}