using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_PlayerData", menuName = "BaseDefenseClone/CD_PlayerData", order = 0)]
    public class CD_PlayerData : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}