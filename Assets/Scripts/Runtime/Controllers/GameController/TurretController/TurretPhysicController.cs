using Runtime.Enums;
using UnityEngine;

namespace Runtime.Controllers
{
    public class TurretPhysicController : MonoBehaviour
    {
        [SerializeField] private TurretManager turretManager;
        [SerializeField] private BoxCollider turretCollider;
        private void OnTriggerEnter(Collider other)
        {
       
             if (other.CompareTag("TurretNPC"))
            {
                Debug.Log("TurretNPC Entered");
                turretManager.OnTurretStateChange(TurretState.AutoTurret);
                turretCollider.excludeLayers  = LayerMask.GetMask("Player");
              
            } 
          
        }


        private void OnTriggerExit(Collider other)
        {
            
            
        }
    }
}