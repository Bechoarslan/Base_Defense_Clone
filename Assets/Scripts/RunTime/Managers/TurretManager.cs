using System;
using RunTime.Keys;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private float _posX;

        

        #endregion

        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

       

        private void UnSubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}