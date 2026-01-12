using Runtime.Enums;
using RunTime.Utilities;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIType> onOpenUIPanel = delegate { };
        public UnityAction onCloseAllPanels = delegate { };
        public UnityAction<UIType> onCloseUIPanel = delegate { };
    }
}