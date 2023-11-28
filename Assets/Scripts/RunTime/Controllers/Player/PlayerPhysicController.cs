
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Controllers.Player
{
   
    public class PlayerPhysicController : MonoBehaviour
    {


        #region Private Variables

        private readonly string _turret = "Turret";
        private readonly string _bulletArea = "BulletArea";
        private readonly string _turretBulletArea = "TurretBulletArea";
        private readonly string _exitArea = "ExitArea";
        private readonly string _enterArea = "EnterArea";
        

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_turret))
            {
                PlayerSignals.Instance.onPLayerInteractWithTurret?.Invoke(other.transform.gameObject);
            }

            if (other.CompareTag(_bulletArea))
            {
                PlayerSignals.Instance.onPlayerInteractWithBulletArea?.Invoke(other.transform);
            }

            if (other.CompareTag(_turretBulletArea))
            {
                
                PlayerSignals.Instance.onPlayerInteractWithTurretBulletArea?.Invoke(other.transform);
            }

            if (other.CompareTag(_enterArea))
            {
                PlayerSignals.Instance.onPlayerInteractEnterArea?.Invoke();
            }

            if (other.CompareTag(_exitArea))
            {
                PlayerSignals.Instance.onPlayerInteractExitArea?.Invoke();
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_turret))
            {
                PlayerSignals.Instance.onPlayerExitInteractWithTurret?.Invoke();
            }
        }
    }
}