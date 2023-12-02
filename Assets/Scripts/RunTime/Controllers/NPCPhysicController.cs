using System;
using RunTime.Managers;
using UnityEngine;

namespace RunTime.Controllers
{
    public class NPCPhysicController : MonoBehaviour
    {
        [SerializeField] private NPCManager npcManager;


        private void OnTriggerEnter(Collider other)
        {
            npcManager._iNpcStates.OnColliderEntered(other);
            
        }
    }
}