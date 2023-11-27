using System.Collections.Generic;
using RunTime.Data.ValueObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace RunTime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerData", menuName = "BaseDefense/CD_PlayerData", order = 0)]
    public class CD_PlayerData : ScriptableObject
    {
       public PlayerData data;
        
        
    }
    
   
}