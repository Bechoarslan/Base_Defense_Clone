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
                turretManager.StartShooting();
            }
            else if (other.CompareTag("Player"))
            {
                Debug.Log("Player Entered");
                turretManager.IsHaveSeater(true);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Exited");
                turretManager.StopAllCoroutines();
            }
            else if (other.CompareTag("Player"))
            {
                turretManager.StopAllCoroutines();
                turretManager.IsHaveSeater(false);
            }
        }
    }
}