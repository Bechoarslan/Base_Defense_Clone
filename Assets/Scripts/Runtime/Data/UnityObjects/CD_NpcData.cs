using Runtime.Data.ValueObjects.NpcData;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_NpcData", menuName = "BaseDefenseClone/CD_NpcData", order = 0)]
    public class CD_NpcData : ScriptableObject
    {
        public NpcData Data;
    }
}