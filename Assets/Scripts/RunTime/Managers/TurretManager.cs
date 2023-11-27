using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Enums.Pool;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace RunTime.Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform stackBullets;
        [SerializeField] private Transform bulletHolder;
        [SerializeField] private GameObject laser;
        
        

        #endregion

        #region Private Variables
        
        private int _bulletCounter = 0;
        private bool _isPlayerInTurret;
        

        

        #endregion

        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPLayerInteractWithTurret += OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret += OnPlayerExitInteractWithTurret;
        }

        private void OnPlayerExitInteractWithTurret() => _isPlayerInTurret = false;
        

        private void OnPlayerInteractWithTurret(GameObject turretObj)
        {
            laser.SetActive(true);
            _isPlayerInTurret = true;
            var countOfBullets = stackBullets.childCount;
            if (countOfBullets <= 0) return;
            var initBulletCount = countOfBullets * 5;
            for (var i = 0; i < initBulletCount; i++)
            {
                var bulletObj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
                bulletObj.transform.parent = bulletHolder;
                bulletObj.transform.position = bulletHolder.position;
                

            }
            StartCoroutine(ReadyToBulletFire());
        }

        private IEnumerator ReadyToBulletFire( )
        {
            for (int i = bulletHolder.childCount; i > 0; i--)
            {
                if (!_isPlayerInTurret) yield break;
                var bulletObj = bulletHolder.GetChild(i - 1).gameObject; // Adjust the delay as needed
                StartCoroutine(FireBullet(bulletObj));
                yield return new WaitForSeconds(0.3f);
            }
        }

        private IEnumerator FireBullet(GameObject bulletObj)
        {
            _bulletCounter++;
            if (_bulletCounter == 5)
            {
                var obj = stackBullets.GetChild(stackBullets.childCount - 1).gameObject;
                obj.SetActive(false);
                PoolSignals.Instance.onSendPool?.Invoke(obj,PoolType.Bullet);
                _bulletCounter = 0;
            }
          
            bulletObj.SetActive(true);
             bulletObj.GetComponent<Rigidbody>().AddForce(bulletObj.transform.forward * 25, ForceMode.VelocityChange);
            
             yield return new WaitForSeconds(0.5f);
             PoolSignals.Instance.onSendPool?.Invoke(bulletObj,PoolType.Projectile);
            
            
            yield return null;

        }


        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPLayerInteractWithTurret += OnPlayerInteractWithTurret;
            PlayerSignals.Instance.onPlayerExitInteractWithTurret += OnPlayerExitInteractWithTurret;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}