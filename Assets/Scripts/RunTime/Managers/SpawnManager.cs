using System;
using System.Collections;
using RunTime.Enums.Pool;
using RunTime.Signals;
using UnityEngine;
using UnityEngine.Events;

namespace RunTime.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void OnPlay()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                
                var enemy = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Enemy);
                enemy.transform.position = (Vector3)NavMeshSignals.Instance.onSendEnemySpawnArea?.Invoke();
                enemy.SetActive(true);
                PoolSignals.Instance.onEnemySpawned?.Invoke();
                enemy.transform.parent = transform.parent;
                yield return new WaitForSeconds(4f);
            }
            
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}