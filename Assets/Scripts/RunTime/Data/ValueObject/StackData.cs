using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Data.ValueObject
{
    [Serializable]
    public struct StackData
    {
        public string DataName;
        public float StackLimit;
        public float StackAnimDuration;
        public float StackOffSet;


        public List<Vector3> _bulletData;

    }
    
}