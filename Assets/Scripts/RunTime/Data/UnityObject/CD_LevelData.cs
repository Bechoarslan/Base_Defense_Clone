using System.Collections.Generic;
using RunTime.Data.ValueObject;
using UnityEngine;

namespace RunTime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_LevelData", menuName = "BaseDefense/CD_LevelData", order = 0)]
    public class CD_LevelData : ScriptableObject
    {
        public List<LevelData> Data;
    }
}