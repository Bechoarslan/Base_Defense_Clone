using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_StackData", menuName = "BaseDefenseClone/CD_StackData", order = 0)]
    public class CD_StackData : ScriptableObject
    {
        public StackData stackData;
    }
}