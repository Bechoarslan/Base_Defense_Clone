using System;
using System.Collections.Generic;
using UnityEngine;

namespace RunTime.Data.ValueObject
{
    [Serializable]
    public struct PlayerData
    {
        public float JoystickSpeed;
        [Header("Stack Data")]
        public List<Stack> StackData;


    }

    [Serializable]
    public struct Stack
    {
        public string DataName;
        public float StackLimit;
        public float StackAnimDuration;
        public float StackOffSet;
        public List<Vector3> BulletData;
    }
}