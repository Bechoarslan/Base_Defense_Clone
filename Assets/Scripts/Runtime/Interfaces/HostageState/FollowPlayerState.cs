using Runtime.Managers.NPCManager.Hostage;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.HostageState
{
    public class FollowPlayerState : INPCStateMachine
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
            Agent.SetDestination(Manager.playerTransform.position);
            Debug.Log("Hostage is Following the Player");

        }

        public void UpdateState()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            Agent.SetDestination(Manager.playerTransform.position);
          
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MiningArea"))
            {
                Debug.Log(("Hostage reached Mining Area"));
            }
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
    }
}