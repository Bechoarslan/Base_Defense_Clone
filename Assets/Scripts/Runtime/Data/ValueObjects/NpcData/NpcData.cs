using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Data.ValueObjects.NpcData
{
    [Serializable]
    public class NpcData
    {
        public float MoveSpeed;
        public float RotationSpeed;
        public float MaxStackCount;
        public float WaitTime;

    }
}