using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerShootingController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Transform LookAtTarget;

        #endregion
        #region Serialized Variables

        [SerializeField] private List<GameObject> enemyList;
        [SerializeField] private Transform firePoint;

        #endregion

        #region Private Variables
        
        private Coroutine _shootingCoroutine;
        private PlayerState _playerState;
        #endregion

        #endregion
        
       

        private IEnumerator StartShootingRoutine()
        {
            var waiter = new WaitForSeconds(1f);
            
            while (_playerState == PlayerState.Shooting)
            {
                
                
                for (int i = enemyList.Count - 1; i >= 0; i--)
                {
                    var enemy = enemyList[i];
                    if (enemy != null)
                    {
                        LookAtTarget = enemy.transform;
                       
                        Fire(); 
                       
                    }
                }
        
                yield return waiter;
            }
            
        }

        private void Fire()
        {
            var bullet = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
            if (bullet != null)
            {
                bullet.transform.SetParent(firePoint);
                bullet.transform.localPosition = Vector3.zero;
                bullet.SetActive(true);
                var rb = bullet.GetComponent<Rigidbody>();
                Vector3 direction = (LookAtTarget.position - firePoint.position).normalized;
                rb.velocity = direction * 50f; // Merminin hızı
               
            }
        }


        private void AddEnemyToList(GameObject otherGameObject) 
        {
            if (!enemyList.Contains(otherGameObject))
            {
                enemyList.Add(otherGameObject);
        
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                AddEnemyToList(other.gameObject.transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (enemyList.Contains(other.gameObject))
                {
                    enemyList.Remove(other.gameObject);
                    
                    
                }
            }
        }

        public void OnStateChanged(PlayerState playerState)
        {
            _playerState = playerState;
            if (_playerState != PlayerState.Shooting)
            {
                if(_shootingCoroutine != null)
                {
                    StopCoroutine(_shootingCoroutine);
                }
            }
            else
            {
                StartCoroutine(StartShootingRoutine());
            }
           
        }
    }
}