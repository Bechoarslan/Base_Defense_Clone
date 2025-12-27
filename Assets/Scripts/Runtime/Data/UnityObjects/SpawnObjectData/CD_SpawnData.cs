using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects.SpawnObjectData
{
    [CreateAssetMenu(fileName = "CD_SpawnData", menuName = "BaseDefenseClone/CD_SpawnData", order = 0)]
    public class CD_SpawnData : ScriptableObject
    {
        public SpawnData Data;
    }
}