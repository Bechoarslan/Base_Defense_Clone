using UnityEngine;

namespace RunTime.Commands.LevelCommands
{
    public struct LevelLoaderCommand
    {
        private readonly GameObject _levelHolder;
        public LevelLoaderCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute(int calculateLevelId)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Datas/LevelPrefabs/Level {calculateLevelId}"),_levelHolder.transform);
        }
    }
}