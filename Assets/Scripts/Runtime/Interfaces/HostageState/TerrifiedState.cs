using Runtime.Controllers.NpcController.Hostage;
using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.HostageState
{
    public class TerrifiedState : INPCStateMachine
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
            Debug.Log(("Hostage is Terrified and Position set to wait point"));
        }

        public void UpdateState()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.tag);
            if (other.CompareTag("Player"))
            {
                Debug.Log("Changing State to Follow Player");
                FollowPlayer(other.transform.root);
            }
        }

        private void FollowPlayer(Transform playerTransform)
        {
            Manager.playerTransform = playerTransform;
            Manager.SwitchState(HostageStateType.FollowPlayer);
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}