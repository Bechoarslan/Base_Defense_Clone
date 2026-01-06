using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers
{
    public class TurretController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform turretTransform;
        [SerializeField] private TurretManager turretManager;
        [SerializeField] private Transform firePoint;
        [SerializeField] private List<GameObject> enteredEnemies = new List<GameObject>();

        #endregion

        #region Private Variables

        private int _ammoCounter;
        private IDamageable _damageable;
         private Transform _targetedEnemy;
        #endregion

        #endregion

       
       

        public IEnumerator EnemyShooting(Transform ammoHolder)
        {
            var waiter = new WaitForSeconds(0.5f);
            
            while (true)
            {
                
                if (ammoHolder.childCount <= 0 || enteredEnemies.Count <= 0 )
                {
                    Debug.Log("Shooting Coroutine Active");
                    yield return waiter;
                    continue;
                }
                
                _ammoCounter++;
                if (_ammoCounter >= 4)
                {
                    var ammoObj = ammoHolder.GetChild(ammoHolder.childCount - 1).gameObject;
                    PoolSignals.Instance.onSendPoolObject?.Invoke(ammoObj, PoolType.Ammo);
                    _ammoCounter = 0;
                }

                var projectile = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.TurretBullet);
 
                projectile.transform.position = firePoint.position;
                projectile.SetActive(true);
                var rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = firePoint.forward * 20f;
                yield return waiter;
            }

        }

        public IEnumerator RotateToEnemy()
        {
            var waiter = new WaitForSeconds(0.1f);
            while (true)
            {

                if (_targetedEnemy != null)
                {
                    var target = _targetedEnemy.position - turretTransform.position;
                    target.y = 0;
                    var lookRotation = Quaternion.LookRotation(target);
                    turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, lookRotation, 0.1f);
                }

                yield return waiter;
            }
        }

       

        public void OnEnterEnemy(GameObject enemyObj)
        {
            if (enteredEnemies.Contains(enemyObj)) return;
            Debug.Log("Added Enemy to List");
            _targetedEnemy = enemyObj.transform;
            enteredEnemies.Add(enemyObj);
            _damageable = enemyObj.GetComponent<IDamageable>();
        }

        public void OnEnemyDiedClearFromList(GameObject obj)
        {
            
            if (!enteredEnemies.Remove(obj)) return;
            if (enteredEnemies.Count == 0)
            {
                _targetedEnemy = null;
                _damageable = null;
                return;
            }
            if (_targetedEnemy != obj.transform) return;
            _targetedEnemy = null;
            _damageable = null;
            float closestDistance = Mathf.Infinity;
            foreach (var enemyPair in enteredEnemies)
            {
                var enemyTransform = enemyPair.transform;
                var distance = Vector3.Distance(transform.position, enemyTransform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _targetedEnemy = enemyTransform;
                    _damageable = enemyPair.GetComponent<IDamageable>();
                }
            }
        }
    }
}