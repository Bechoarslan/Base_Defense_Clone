using System;
using Runtime.Keys;
using RunTime.Utilities;
using UnityEngine;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    { 
        public Action<InputParamsKeys> onInputParamsChanged;
        public Action<bool> onInputReadyToPlay;

    }
}