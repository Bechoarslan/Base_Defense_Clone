using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Data.UnityObjects.SpawnObjectData;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Managers.NPCManager.NPCMoneyCollector;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [Header("Enemy Spawn Points")]
        [SerializeField] private Transform enemyHolder;
        [SerializeField] private List<Transform> enemySpawnPoints;
        [SerializeField] private CD_SpawnData spawnData;
        [SerializeField] private List<Transform> enemyWalkPoints;

        [Header("NPC Money Collector Points")]
        [SerializeField] private Transform npcMoneyCollectorWalkPoint;


        [Header("Hostage Spawn Points")] [SerializeField]
        private List<Transform> _hostageSpawnPoint;
        #endregion

        #region Private Variables

        private SpawnData _spawnData;
        #endregion

        #endregion


        private void Awake()
        {
            _spawnData = spawnData.Data;
        }

        public void Start()
        {
            SpawnHostages();
            StartCoroutine(StartSpawn());
        }

        private void SpawnHostages()
        {
            for (int i = 0; i < _hostageSpawnPoint.Count; i++)
            {
                var hostage = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.NPCHostage);
                hostage.transform.SetParent(_hostageSpawnPoint[i]);
                hostage.transform.position = _hostageSpawnPoint[i].position;
                hostage.SetActive(true);
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onSendNPCMoneyCollectorWalkPoint += OnSendCollectorWaitPoint;
            GameSignals.Instance.onEnemyWalkPointTransform += SendEnemyWalkPoints;
            GameSignals.Instance.onAddNewEnemyWalkPoint += OnAddNewEnemyWalkPoint;
         
            
        }

    

        private Transform OnSendCollectorWaitPoint() => npcMoneyCollectorWalkPoint;
        

        private Transform SendEnemyWalkPoints()
        {

            for (int i = 0; i < enemyWalkPoints.Count; i++)
            {
                var randomPoint = UnityEngine.Random.Range(0, enemyWalkPoints.Count);
                if (!enemyWalkPoints[randomPoint].gameObject.activeInHierarchy)
                {
                    continue;
                }
                return enemyWalkPoints[randomPoint];;

            }
            

            return null;
        }


        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onEnemyWalkPointTransform -= SendEnemyWalkPoints;
            GameSignals.Instance.onAddNewEnemyWalkPoint -= OnAddNewEnemyWalkPoint;
            GameSignals.Instance.onSendNPCMoneyCollectorWalkPoint -= OnSendCollectorWaitPoint;
        }

        private void OnAddNewEnemyWalkPoint(Transform arg0)
        {
            enemyWalkPoints.Add(arg0);
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private IEnumerator StartSpawn()
        {
            var wait = new WaitForSeconds(_spawnData.SpawnDelay);
            while (true)
            {
                SpawnEnemy();
                yield return wait;
            }
        }

        private void SpawnEnemy()
        {
            for (int i = 0; i < _spawnData.SpawnCount; i++)
            {
                var enemyObj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Enemy);
                enemyObj.transform.SetParent(enemyHolder);
                enemyObj.transform.position = enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Count)].position;
                enemyObj.SetActive(true);
                
            }
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<SpawnManager>();
        }
    }
}