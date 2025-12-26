using System.Collections.Generic;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_PoolData", menuName = "BaseDefenseClone/CD_PoolData", order = 0)]
    public class CD_PoolData : ScriptableObject
    {
        public List<PoolData> pools;
    }
}