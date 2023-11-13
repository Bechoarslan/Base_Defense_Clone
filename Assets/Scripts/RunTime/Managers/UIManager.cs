using System;
using RunTime.Enums;
using RunTime.Enums.UIEnums;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Managers
{
    public class UIManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            OpenStartPanel();
            CoreGameSignals.Instance.onLevelInitialize += () => 
                CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level,1);
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelFailed += () =>
                CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail,2);
            CoreGameSignals.Instance.onLevelSuccesfull += () =>
                CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
        }

        public void OnPlay()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level,1);
            CoreGameSignals.Instance.onPlay?.Invoke();
            CoreUISignals.Instance.onClosePanel?.Invoke(0);
           
        }


        private void UnSubscribeEvents()
        {
          
            CoreGameSignals.Instance.onLevelInitialize -= () => 
                CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level,1);
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelFailed -= () =>
                CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail,2);
            CoreGameSignals.Instance.onLevelSuccesfull -= () =>
                CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
        }

        private void OpenStartPanel() => CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start,0);

        public void OnNextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void OnRestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }
       

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnReset()
        {
            CoreUISignals.Instance.onCloseAllPanels?.Invoke();
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start,0);
        }
    }
}