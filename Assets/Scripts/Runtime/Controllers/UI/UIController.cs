using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private Dictionary<UIType,GameObject> _uiPanelDictionary = new Dictionary<UIType, GameObject>();
        #endregion

        #endregion

        public void OpenPanel(UIType uiType)
        {
            
            if (_uiPanelDictionary.ContainsKey(uiType)) return;
            Debug.Log(uiType);
            var panel = Resources.Load<GameObject>("UI/" + uiType.ToString());
            var instantiatedPanel = Instantiate(panel, transform);
            _uiPanelDictionary.Add(uiType, instantiatedPanel);

        }

        public void CloseAllPanels()
        {
            if(_uiPanelDictionary.Count==0) return;
            foreach (var panel in _uiPanelDictionary)
            {
                _uiPanelDictionary.Remove(panel.Key);
                Destroy(panel.Value);
            }
        }

        public void CloseUIPanel(UIType uiType)
        {
            if(!_uiPanelDictionary.TryGetValue(uiType, out var value)) return;
            Destroy(value);
            _uiPanelDictionary.Remove(uiType);
        }
    }
}