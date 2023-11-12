using UnityEngine;

namespace RunTime.Commands.LevelCommands
{
    public struct LevelDestroyerCommand
    {
        private readonly GameObject _levelHolder;
        public LevelDestroyerCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }
    }
}