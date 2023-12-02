using RunTime.Enums.NPC;
using UnityEngine;

namespace RunTime.Interfaces
{
    public interface INpcStates
    {

        void StartAction();

        void UpdateAction();

        void OnColliderEntered(Collider other);

        void SetAnimation(NPCAnimationEnum animationEnums);



    }

   
}