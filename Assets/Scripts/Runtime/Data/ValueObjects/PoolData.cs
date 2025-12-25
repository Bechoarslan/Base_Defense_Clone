using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public class PoolData
    {
        public PoolType poolType;
        public int poolSize;
        public GameObject poolPrefab;

    }
}