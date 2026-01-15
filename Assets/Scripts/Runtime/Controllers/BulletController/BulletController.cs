using System;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.BulletController
{
    public class BulletController : MonoBehaviour
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

                PoolSignals.Instance.onSendPoolObject?.Invoke(this.gameObject,PoolType.Bullet);
                
                
               
            }
        }
    }
}