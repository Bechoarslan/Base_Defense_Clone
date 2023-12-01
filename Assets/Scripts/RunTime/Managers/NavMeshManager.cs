using System;
using System.Collections.Generic;
using RunTime.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunTime.Managers
{
    public class NavMeshManager : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private List<Transform> enemySpawnArea;
        [SerializeField] private Transform playerWentArea;

        #endregion


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            NavMeshSignals.Instance.onSendEnemySpawnArea += SendWallsTransformToEnemy;
            NavMeshSignals.Instance.onSendEnemyWalkArea += SendEnemyWalkArea;
        }

        private Transform SendEnemyWalkArea()
        {
            return playerWentArea;
        }

        private Vector3 SendWallsTransformToEnemy()
        {
                
                var randomPosZ = Random.Range(enemySpawnArea[0].position.z, enemySpawnArea[1].position.z);
                var randomPosX = Random.Range(enemySpawnArea[0].position.x, enemySpawnArea[2].position.x);
                var newPos = new Vector3(randomPosX, 0, randomPosZ);
                return newPos;
        }

        private void UnSubscribeEvents()
        {
            NavMeshSignals.Instance.onSendEnemySpawnArea -= SendWallsTransformToEnemy;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}