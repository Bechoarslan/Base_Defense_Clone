using System;
using System.Collections.Generic;
using RunTime.Enums;
using RunTime.Enums.UIEnums;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEditor.iOS;
using UnityEngine;

namespace RunTime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> layers = new List<GameObject>();

        #endregion

        #endregion


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
        }

        [Button("OnCloseAllPanels")]
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                for (int i = 0; i < layer.transform.childCount; i++)
                {
                    Destroy(layer.transform.GetChild(i).gameObject);
                    
                }
            }
        }

        [Button("OnClosePanel")]
        private void OnClosePanel(int layerValue)
        {
            if (layers[layerValue].transform.childCount > 0)
            {
                for (int i = 0; i < layers[layerValue].transform.childCount; i++)
                {
                    Destroy(layers[layerValue].transform.GetChild(i).gameObject);
                    
                }
            }
        }

        [Button("OnOpenPanel")]
        private void OnOpenPanel(UIPanelTypes panel, short layerValue)
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(layerValue);
             Instantiate(Resources.Load<GameObject>($"Data/UIPanels/{panel}Panel"), layers[layerValue].transform);


        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}