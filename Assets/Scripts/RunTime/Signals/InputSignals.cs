using RunTime.Extensions;
using RunTime.Keys;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputTaken = delegate {  };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate {  };
        public UnityAction onInputReleased = delegate {  };

    }
}