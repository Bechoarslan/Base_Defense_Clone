using RunTime.Data.ValueObject;
using UnityEngine;

namespace RunTime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_InputData", menuName = "BaseDefense/CD_InputData", order = 0)]
    public class CD_InputData : ScriptableObject
    {
        public InputData Data;
    }
}