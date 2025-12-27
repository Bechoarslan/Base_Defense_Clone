using Runtime.Data.ValueObjects.NpcData;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_EnemyData", menuName = "BaseDefenseClone/CD_EnemyData", order = 0)]
    public class CD_EnemyData : ScriptableObject
    {
        public EnemyData Data;
    }
}