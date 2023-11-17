using Cinemachine;

namespace Runtime.Extentions
{
    public class CinemachineLockAxis : CinemachineExtension
    {
        public float XPosition = 0;
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = XPosition;
                state.RawPosition = pos;

            }
        }
    }
}