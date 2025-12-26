using Runtime.Managers.NPCManager.Hostage;
using UnityEngine;

namespace Runtime.Controllers.NpcController.BulletCarrier
{
    public class BulletCarrierController : MonoBehaviour
    {
        [SerializeField] private NPCBulletCarrierManager manager;
        
        private void OnTriggerEnter(Collider other)
        {
            
            manager.CurrentState.OnTriggerEnter(other);
        }
    }
}