using Runtime.Managers.NPCManager.Hostage;
using UnityEngine;

namespace Runtime.Controllers.NpcController.Hostage
{
    public class NPCHostageController : MonoBehaviour
    {
        [SerializeField] private NPCHostageManager npcHostageManager;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hostage OnTriggerEnter");
            npcHostageManager._currentState.OnStateTriggerEnter(other);
        }
    }
}