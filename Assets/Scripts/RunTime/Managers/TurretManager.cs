using System;
using System.Collections;
using System.Collections.Generic;
using RunTime.Enums.Pool;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform stackBullets;
        [SerializeField] private Transform turretAim;
        private int xx = 0;
        

        #endregion

        #region Private Variables
        

        

        #endregion

        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPLayerInteractWithTurret += OnPlayerInteractWithTurret;
        }

        private void OnPlayerInteractWithTurret(GameObject turretObj)
        {
            var countOfBullets = stackBullets.childCount;
            if (countOfBullets <= 0) return;
            var initBulletCount = countOfBullets * 5;
            for (var i = 0; i < initBulletCount; i++)
            {
                var bulletObj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
                bulletObj.transform.parent = turretAim;
                bulletObj.transform.position = turretAim.position;
                

            }
            StartCoroutine(PlayBullet());
        }

        private IEnumerator PlayBullet( )
        {
            for (int i = turretAim.childCount; i > 0; i--)
            {
                
                var bulletObj = turretAim.GetChild(i - 1).gameObject;
                yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
                StartCoroutine(BulletPlay(bulletObj));
            }
        }

        private IEnumerator BulletPlay(GameObject bulletObj)
        {
            xx++;
            if (xx == 5)
            {
                var obj = stackBullets.GetChild(stackBullets.childCount - 1).gameObject;
                obj.SetActive(false);
                PoolSignals.Instance.onSendPool?.Invoke(obj,PoolType.Bullet);
                
                xx = 0;
            }
            
          
            bulletObj.SetActive(true);
            bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 40 ;
            yield return new WaitForSeconds(3f);
            bulletObj.SetActive(false);
            
            PoolSignals.Instance.onSendPool?.Invoke(bulletObj, PoolType.Projectile);
        }


        private void UnSubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}