using System;
using UnityEngine;

namespace RunTime.Data.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public string ObjName;
        public GameObject Prefab;
        public int ObjectCount;

    }
}