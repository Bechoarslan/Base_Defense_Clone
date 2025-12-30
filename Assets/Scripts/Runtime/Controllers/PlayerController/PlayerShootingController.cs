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

        #region Serialized Variables

        [SerializeField] private List<GameObject> enemyList;

        #endregion

        #region Private Variables
        
        private Coroutine _shootingCoroutine;
        #endregion

        #endregion

        // Inspector'da görünecek buton bu fonksiyonda olmalı
        [Button("Start Shooting")] 
        private void StartShootingButton()
        {
            // Oyun çalışmıyorsa Coroutine başlatılamaz, hata almamak için kontrol ekle
            if (Application.isPlaying)
            {
                StopAllCoroutines(); // Üst üste binmemesi için öncekileri durdurabilirsin
                StartCoroutine(StartShootingRoutine());
            }
            else
            {
                Debug.LogWarning("Coroutine'ler sadece Play Mode'da çalışır!");
            }
        }

// Asıl mantık burada (İsmini karışmasın diye değiştirdim)
        private IEnumerator StartShootingRoutine()
        {
            var waiter = new WaitForSeconds(1f);
            Debug.Log("In Shooting Coroutine - Başladı");
    
            // HATA AYIKLAMA: Listenin o anki durumunu konsola yazdır
            Debug.Log($"Enemy List Count: {enemyList.Count}"); 

            while (enemyList.Count > 0)
            {
                Debug.Log("Shooting at enemies - Döngüye Girdi");
        
                // Foreach yerine for kullanmak, ateş ederken düşman ölürse (listeden silinirse)
                // "CollectionModified" hatası almanızı engeller.
                for (int i = enemyList.Count - 1; i >= 0; i--)
                {
                    var enemy = enemyList[i];
                    if (enemy != null)
                    {
                        var enemyTransform = enemy.transform;
                        Fire(); 
                        Debug.Log($"Shooting at enemy: {enemyTransform.name}");
                    }
                }
        
                yield return waiter;
            }
    
            Debug.Log("Enemy List 0 olduğu için döngüden çıkıldı.");
        }

        private void Fire()
        {
            var bullet = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Projectile);
            if (bullet != null)
            {
                bullet.transform.position = transform.position + transform.forward * 1.5f + Vector3.up * 1.0f;
                bullet.transform.rotation = Quaternion.LookRotation(transform.forward);
                bullet.SetActive(true);
                var rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = transform.forward * 20f; // Set bullet speed
            }
        }


        public void AddEnemyToList(GameObject otherGameObject) 
        {
            if (!enemyList.Contains(otherGameObject))
            {
                enemyList.Add(otherGameObject);
                Debug.Log($"Enemy added to list: {otherGameObject.name}");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                StartCoroutine(StartShootingRoutine());
            }
        }
    }
}