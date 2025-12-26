using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers
{
    public class TurretController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TurretManager turretManager;
        [SerializeField] private Transform firePoint;

        #endregion

        #region Private Variables

        private int _ammoCounter;
        #endregion

        #endregion

       
       

        public IEnumerator EnemyShooting(Transform ammoHolder)
        {
            var waiter = new WaitForSeconds(0.5f);
            
            while (true)
            {
                if (ammoHolder.childCount <= 0 || turretManager.enteredEnemies.Count <= 0)
                {
                    yield return null;
                    continue;
                }
                _ammoCounter++;
                if (_ammoCounter >= 4)
                {
                    var ammoObj = ammoHolder.GetChild(ammoHolder.childCount - 1).gameObject;
                    PoolSignals.Instance.onSendPoolObject?.Invoke(ammoObj, PoolType.Ammo);
                    _ammoCounter = 0;
                }

                var projectile = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
                projectile.transform.position = firePoint.position;
                projectile.SetActive(true);
                projectile.transform.DOMove(new Vector3(firePoint.position.x, firePoint.position.y, 10f), 0.5f);
                yield return waiter;
            }

        }
    }
}