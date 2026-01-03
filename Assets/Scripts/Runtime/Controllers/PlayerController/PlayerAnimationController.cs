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


    

        public void OnChangeBaseLayer(int layerIndex, float value)
        {
            animator.SetLayerWeight(layerIndex, value);
            
        }

        public void OnChangeSetAnimFloat(float value, PlayerAnimState floatName)
        {
           animator.SetFloat(floatName.ToString(), value);
        }

        public void OnTriggerAnimation(PlayerAnimState obj)
        {
            
            animator.SetTrigger(obj.ToString());
        }
        
        public void OnChangeAnimBool(bool value, PlayerAnimState boolName)
        {
            Debug.Log("Bool Changed "+ boolName.ToString() + " Value: " + value);
            animator.SetBool(boolName.ToString(), value);
        }
    }
}