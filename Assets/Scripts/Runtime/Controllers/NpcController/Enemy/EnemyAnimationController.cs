using Runtime.Enums.EnemyStateType;
using UnityEngine;

namespace Runtime.Controllers.NpcController.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        #endregion

        #endregion


     
        public void OnSetTriggerAnimation(EnemyStateType stateType)
        {
            animator.SetTrigger(stateType.ToString());
        }




    }
}