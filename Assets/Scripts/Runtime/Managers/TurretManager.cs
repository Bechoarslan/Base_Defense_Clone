using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Controllers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform turret;
        [SerializeField] private Transform standPoint;
        [SerializeField] private Transform ammoHolder;
        [SerializeField] private Transform firePoint;
        #endregion

        #region Private Variables

        [SerializeField]private List<GameObject> _enteredEnemies = new List<GameObject>();
        private int ammoCounter;
        private bool _isHaveSeater;
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onGetTurretStandPointAndTurretTransform += OnGetTurretStandPoint;
            StackSignals.Instance.onGetTurretHolderTransform += OnGetHolderTransform;
        }

        private Transform OnGetHolderTransform() => ammoHolder;
       
        

        private (Transform ,Transform) OnGetTurretStandPoint()
        {
            return (turret, standPoint);
        }


        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onGetTurretStandPointAndTurretTransform -= OnGetTurretStandPoint;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public void StartShooting()
        {
            Debug.Log("Cant Shoot something wrong");
            if (!_isHaveSeater && ammoHolder.childCount < 1) return;
            Debug.Log("Starting Shooting");
            StartCoroutine(EnemyShooting());
        }

        private IEnumerator EnemyShooting()
        {
            var waiter = new WaitForSeconds(0.5f);
            
            while ((ammoHolder.childCount - 1) * 4 > 1)
            {
                ammoCounter++;
                if (ammoCounter >= 4)
                {
                    Debug.Log("Out of Ammo");
                   var ammoObj = ammoHolder.GetChild(ammoHolder.childCount - 1).gameObject;
                     PoolSignals.Instance.onSendPoolObject?.Invoke(ammoObj,PoolType.Ammo);
                    ammoCounter = 0;
                }
                var projectile = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
                projectile.transform.position = firePoint.position;
                projectile.SetActive(true);
                projectile.transform.DOMove( new Vector3(firePoint.position.x, firePoint.position.y,10f), 0.5f);
               yield return waiter;
            }
            
            
        }

        public void IsHaveSeater(bool value)
        {
            _isHaveSeater = value;
            if (_isHaveSeater && _enteredEnemies.Count > 0)
            {
                StartCoroutine(EnemyShooting());
            }
            
        }

        public void AddEnemyToList(GameObject otherGameObject) => _enteredEnemies.Add(otherGameObject);
       
    }
}