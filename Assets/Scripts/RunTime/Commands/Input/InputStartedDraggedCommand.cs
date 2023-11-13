using RunTime.Managers;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Commands.Input
{
    public class InputStartedDraggedCommand
    {
        private bool _isfirstTimeTouchTaken = true;
        private InputManager _inputManager;
        public InputStartedDraggedCommand(ref bool isFirstTimeTouchTaken, InputManager inputManager)
        {
            _isfirstTimeTouchTaken = isFirstTimeTouchTaken;
            _inputManager = inputManager;
        }

        public void Execute()
        {
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isfirstTimeTouchTaken)
            {
                _isfirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }

            _inputManager.MousePosition = UnityEngine.Input.mousePosition;
        }
    }
}