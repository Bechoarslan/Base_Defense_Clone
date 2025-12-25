using System;
using Runtime.Enums;
using RunTime.Utilities;
using UnityEngine;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Action<PlayerState> onChangePlayerState = delegate { };
        public Action<Transform> onSendStacksToHolder = delegate { };
    }
}