using RunTime.Enums.NPC;
using RunTime.Interfaces;
using RunTime.Managers;
using RunTime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace RunTime.State.Enemy
{
    public class EnemyRunPlayerState : INpcStates
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private NPCManager _npcManager;
        public EnemyRunPlayerState(ref NavMeshAgent navMeshAgent, ref Animator animator, NPCManager npcManager)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _npcManager = npcManager;
        }

        public void StartAction()
        {
            SetAnimation(NPCAnimationEnum.Run);
        }

        public void UpdateAction()
        {
            
            var playerTransform = PlayerSignals.Instance.onSendPlayerTransform?.Invoke();
            _npcManager.transform.LookAt(playerTransform);
            _navMeshAgent.SetDestination(playerTransform.position);
            if (_navMeshAgent.remainingDistance < 1f)
            {
                _npcManager.ChangeCurrentState(NPCStateType.EnemyAttack);
            }
        }

        public void OnColliderEntered(Collider other)
        {
            
        }

        public void SetAnimation(NPCAnimationEnum animationEnums)
        {
            _animator.SetTrigger(animationEnums.ToString());
        }
        

        public void SetAnimation()
        {
            
        }
    }
}