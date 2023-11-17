using RunTime.Enums.Camera;
using RunTime.Extensions;
using UnityEditor.Rendering.LookDev;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction<CameraEnums> onChangeCameraState = delegate {  };
        public UnityAction onSetCinemachineTarget = delegate {  };
    }
}