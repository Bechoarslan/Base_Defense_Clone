using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Serialized Variables

        [SerializeField] private Animator animator;
     

        #endregion


        public void OnChangeAnimationBool(bool value, PlayerAnimState boolName)
        {
            animator.SetBool(boolName.ToString(), value);
        }

        public void OnChangeBaseLayer(int layerIndex, float value)
        {
            animator.SetLayerWeight(layerIndex, value);
            
        }

    }
}