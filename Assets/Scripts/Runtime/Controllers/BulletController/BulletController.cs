using System;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Controllers.BulletController
{
    public class BulletController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Bullet hit an enemy!" + other.gameObject.transform.root.gameObject.name);
                
                // Burada düşmana hasar verme işlemi yapılabilir
                var idamageable = other.transform.parent.GetComponent<IDamageable>();
                if (idamageable != null)
                {
                    idamageable.TakeDamage(20);
                }

                // Mermiyi yok et
               
            }
        }
    }
}