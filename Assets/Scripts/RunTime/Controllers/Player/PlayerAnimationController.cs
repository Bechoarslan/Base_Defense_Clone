using RunTime.Keys;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;

        #endregion

        #region Private Variables

        [ShowInInspector] private float _posX;
        [ShowInInspector] private float _posZ;
        private HorizontalInputParams _inputParams;

        #endregion

        #endregion

        private void SetIdleRunState(float posX)
        {
                playerAnimator.SetFloat("PosX",posX);
        }

        public void UpdateInputParams(HorizontalInputParams inputParams)
        {
            _inputParams = inputParams;
            SetIdleRunState(_inputParams.Values.x);
        }
    }
}