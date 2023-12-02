using System;
using RunTime.Enums.NPC;
using RunTime.Interfaces;
using RunTime.Managers;
using RunTime.Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace RunTime.State.Enemy
{
    public class EnemyAttackState : INpcStates
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private NPCManager _npcManager;
        public EnemyAttackState(ref NavMeshAgent navMeshAgent, ref Animator animator, NPCManager npcManager)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _npcManager = npcManager;
        }

        public void StartAction()
        {
            var playerTransform = PlayerSignals.Instance.onSendPlayerTransform?.Invoke();
            _npcManager.transform.LookAt(playerTransform);
            SetAnimation(NPCAnimationEnum.Attack);
            
        }

        public void UpdateAction()
        {
            var playerTransform = PlayerSignals.Instance.onSendPlayerTransform?.Invoke();
           if ((_navMeshAgent.transform.position - playerTransform.transform.position).sqrMagnitude > 1f)
           {
               _npcManager.ChangeCurrentState(NPCStateType.EnemyRun);
           }
            
            
        }

        public void OnColliderEntered(Collider other)
        {
            
        }

        public void SetAnimation(NPCAnimationEnum animationEnums)
        {
            _animator.SetTrigger(animationEnums.ToString());
        }
    }
}