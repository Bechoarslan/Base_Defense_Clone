using RunTime.Enums;
using RunTime.Enums.UIEnums;
using RunTime.Extensions;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class CoreUISignals : MonoSingleton<CoreUISignals>
    {
        public UnityAction<UIPanelTypes,short> onOpenPanel = delegate {  };
        public UnityAction<int> onClosePanel = delegate {  };
        public UnityAction onCloseAllPanels = delegate {  };
    }
}