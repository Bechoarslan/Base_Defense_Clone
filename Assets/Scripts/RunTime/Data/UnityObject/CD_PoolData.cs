using System.Collections.Generic;
using RunTime.Data.ValueObject;
using UnityEngine;

namespace RunTime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PoolData", menuName = "BaseDefense/CD_PoolData", order = 0)]
    public class CD_PoolData : ScriptableObject
    {
        public List<PoolData> Data;
    }
}