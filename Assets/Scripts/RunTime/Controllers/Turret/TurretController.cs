using System.Collections;
using System.Collections.Generic;
using RunTime.Enums.Pool;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Controllers.Turret
{
    public class TurretController : MonoBehaviour
    {
        #region Private Variables

        private int _bulletCounter = 0;
        private bool _isPlayerInTurret;

        #endregion
        
        internal void OnPlayerExitInteractWithTurret() => _isPlayerInTurret = false;
        
       

        public void OnPlayerInteractWithTurret(Transform bulletBoxArea, Transform bulletHolder, GameObject laser)
        {
            laser.SetActive(true);
            _isPlayerInTurret = true;

           
            var countOfBullets = bulletBoxArea.childCount;
            if (countOfBullets <= 0) return;
            var initBulletCount = countOfBullets * 5;
            for (var i = 0; i < initBulletCount; i++)
            {
                var bulletObj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
                bulletObj.transform.parent = bulletHolder;
                bulletObj.transform.position = bulletHolder.position;
                
            }

            StartCoroutine(ReadyToBulletFire(bulletHolder,bulletBoxArea));
        }
        
        private IEnumerator ReadyToBulletFire(Transform bulletHolder,Transform bulletBoxArea)
        {
            for (int i = bulletHolder.childCount; i > 0; i--)
            {
                if (!_isPlayerInTurret) yield break;
                var bulletObj = bulletHolder.GetChild(i - 1).gameObject; // Adjust the delay as needed
                StartCoroutine(FireBullet(bulletObj,bulletBoxArea));
                yield return new WaitForSeconds(0.3f);
            }
        }
        
        private IEnumerator FireBullet(GameObject bulletObj,Transform bulletBoxArea)
        {
            _bulletCounter++;
            if (_bulletCounter == 5)
            {
                var obj = bulletBoxArea.GetChild(bulletBoxArea.childCount - 1).gameObject;
                obj.SetActive(false);
                PoolSignals.Instance.onSendPool?.Invoke(obj,PoolType.Bullet);
                _bulletCounter = 0;
            }
            bulletObj.SetActive(true);
            bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * 25, ForceMode.VelocityChange);
            
            yield return new WaitForSeconds(0.5f);
            bulletObj.SetActive(false);
            PoolSignals.Instance.onSendPool?.Invoke(bulletObj,PoolType.Projectile);
            
            
            yield return null;

        }
    }
}