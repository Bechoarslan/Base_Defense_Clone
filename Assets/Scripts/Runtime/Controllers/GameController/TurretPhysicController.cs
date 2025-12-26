using UnityEngine;

namespace Runtime.Controllers
{
    public class TurretPhysicController : MonoBehaviour
    {
        [SerializeField] private TurretManager turretManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Entered");
                turretManager.AddEnemyToList(other.gameObject);
               turretManager.ReadyToShoot();
            }
          
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Exited");
                turretManager.StopAllCoroutines();
            }
            
        }
    }
}