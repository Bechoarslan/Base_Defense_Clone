using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.HostageState
{
    public class FollowPlayerState : IStateMachine
    {
        public NavMeshAgent Agent;
        public NPCHostageManager Manager;
        public FollowPlayerState(NPCHostageManager npcHostageManager, ref NavMeshAgent navMeshAgent)
        {
            Manager = npcHostageManager;
            Agent = navMeshAgent;
        }


        public void EnterState()
        {
            Manager.SetTriggerAnimation(nameof(HostageAnimState.Run));
            Agent.SetDestination(Manager.playerTransform.position);
       

        }

        public void UpdateState()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            Agent.destination = Manager.playerTransform.position;
            if (Agent.pathPending) return;
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                Manager.SetBoolAnimation(nameof(HostageAnimState.IsRunning), false);
                Agent.isStopped = true;
            }
            else
            {
                Manager.SetBoolAnimation(nameof(HostageAnimState.IsRunning), true);
                Agent.isStopped = false;
            }

        }

        public void OnStateTriggerEnter(Collider other)
        {
            
        }

        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            
        }
    }
}