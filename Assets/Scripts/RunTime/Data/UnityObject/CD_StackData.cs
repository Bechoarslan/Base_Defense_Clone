using System.Collections.Generic;
using RunTime.Data.ValueObject;
using UnityEngine;

namespace RunTime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_StackData", menuName = "BaseDefense/CD_StackData", order = 0)]
    public class CD_StackData : ScriptableObject
    {
        
        public List<StackData> Data;
    }
}