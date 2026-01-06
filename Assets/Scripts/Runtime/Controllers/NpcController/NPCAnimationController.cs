using System;
using UnityEngine;

namespace Runtime.Controllers.NpcController
{
    public class NPCAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;


        public void OnTriggerAnimation(String animationName)
        {
            playerAnimator.SetTrigger(animationName);
        }
        
        public void OnChangeBoolAnimation(String animationName, bool value)
        {
            playerAnimator.SetBool(animationName, value);
        }
        
        
    }
}