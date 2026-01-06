using Runtime.Controllers.NpcController.Hostage;
using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.HostageState
{
    public class TerrifiedState : IStateMachine
    {
        private NPCHostageManager Manager;
        private NavMeshAgent Agent;
        public TerrifiedState(NPCHostageManager npcHostageManager, ref NavMeshAgent navMeshAgent)
        {
            Manager = npcHostageManager;
            Agent = navMeshAgent;
        }


        public void EnterState()
        {
            Manager.SetTriggerAnimation(nameof(NPCHostageStateType.Terrified));
            
        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Player"))
            {
                
                FollowPlayer(other.transform.root);
            }
        }

        private void FollowPlayer(Transform playerTransform)
        {
            Manager.playerTransform = playerTransform;
            Manager.SwitchState(NPCHostageStateType.FollowPlayer);
        }

        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            
        }
    }
}