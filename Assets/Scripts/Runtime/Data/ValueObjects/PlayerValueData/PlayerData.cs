using System;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public class PlayerData
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public float TurretMoveSpeed;
        public float Health;
        public float FireRate;
        public float StackLimit;

    }
}