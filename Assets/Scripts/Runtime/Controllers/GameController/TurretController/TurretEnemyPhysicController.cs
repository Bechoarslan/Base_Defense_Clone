using System;
using UnityEngine;

namespace Runtime.Controllers
{
    public class TurretEnemyPhysicController : MonoBehaviour
    {
        [SerializeField] private TurretController turretController;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
             
                turretController.OnEnterEnemy(other.gameObject.transform.parent.gameObject);
            }
        }
    }
}