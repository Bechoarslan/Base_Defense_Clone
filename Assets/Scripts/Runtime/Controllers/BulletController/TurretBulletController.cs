using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.BulletController
{
    public class TurretBulletController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
              
                
                var idamageable = other.transform.parent.GetComponent<IDamageable>();
                if (idamageable != null)
                {
                    idamageable.TakeDamage(20);
                }
                
               
            }
        }
    }
}