using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Interfaces;
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

        public Transform EnemyTarget;

        #endregion
        #region Serialized Variables

        private Dictionary<GameObject,IDamageable> _enemyDictionary = new Dictionary<GameObject, IDamageable>();
        [SerializeField] private Transform firePoint;

        #endregion

        #region Private Variables
        
        private Coroutine _shootingCoroutine;
        private PlayerState _playerState;
        private IDamageable _currentDamageable;
        #endregion

        #endregion
        
       

        public void StartShootingCoroutineCaller()
        {
            _shootingCoroutine = StartCoroutine(StartShootingRoutine());
        }
        private IEnumerator StartShootingRoutine()
        {
            var waiter = new WaitForSeconds(1f);
            Debug.Log("Shooting Coroutine Started");
            while (true)
            {
                Debug.Log("Shooting Coroutine Working");
                Fire();
                yield return waiter;
            }
            
        }

        private void Fire()
        {
            var bullet = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
            if (bullet != null && EnemyTarget != null)
            {
             
                bullet.transform.SetParent(firePoint);
                bullet.transform.localPosition = Vector3.zero;
                bullet.SetActive(true);
                var rb = bullet.GetComponent<Rigidbody>();
                Vector3 direction = (EnemyTarget.position - firePoint.position).normalized;
                rb.velocity =  direction * 50; // Merminin hızı
                bullet.transform.SetParent(null);

            }
         
        }


   

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {   PlayerSignals.Instance.onChangeAnimBool?.Invoke(true,PlayerAnimState.IsShooting);
                var enemy = other.gameObject.transform.parent.gameObject;
                var damageable = enemy.GetComponent<IDamageable>();
                Debug.Log(enemy.name);
                if (_enemyDictionary.ContainsKey(enemy)) return;
                if (EnemyTarget == null)
                { 
                    AddToDictionary(enemy,damageable);
                    EnemyTarget = enemy.transform;
                    
                 
                }
                else
                {
                    AddToDictionary(enemy,damageable);
                    var targetEnemyDistance = Vector3.Distance(transform.position, EnemyTarget.position);
                    var newEnemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (!(newEnemyDistance < targetEnemyDistance)) return;
                    EnemyTarget = enemy.transform;
                    _currentDamageable = damageable;
                }
             
            }
        }

        private void AddToDictionary(GameObject enemy,IDamageable damageable)
        {
            if(damageable.Health <= 0) return;
            _enemyDictionary.TryAdd(enemy,damageable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy =  other.gameObject.transform.parent.gameObject;
                OnEnemyDiedClearFromDic(enemy);
                
            }
        }

    
        public void OnEnemyDiedClearFromDic(GameObject obj)
        {
            if (!_enemyDictionary.Remove(obj)) return;
            if (EnemyTarget != obj.transform) return;
            if (_enemyDictionary.Count == 0)
            {
                PlayerSignals.Instance.onChangeAnimBool?.Invoke(false,PlayerAnimState.IsShooting);
            }
            EnemyTarget = null;
            _currentDamageable = null;
            float closestDistance = Mathf.Infinity;
            foreach (var enemyPair in _enemyDictionary)
            {
                var enemyTransform = enemyPair.Key.transform;
                var distance = Vector3.Distance(transform.position, enemyTransform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    EnemyTarget = enemyTransform;
                    _currentDamageable = enemyPair.Value;
                }
            }



        }

       
        public void StopShootingCoroutineCaller()
        {
            if (_shootingCoroutine == null) return;
           Debug.Log("Shooting Coroutine Stopped");
            StopCoroutine(_shootingCoroutine);
            
        }


        
    }
}