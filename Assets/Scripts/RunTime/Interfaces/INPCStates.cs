using RunTime.Enums.NPC;

namespace RunTime.Interfaces
{
    public interface INPCStates
    {
        void PerformAction();
        
        void SetAnimationState(NPCAnimationState animationState);



    }
}