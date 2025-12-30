using Runtime.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject healthHolder;
  

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public void OnTakeDamage(float damageAmount)
        {
            if (playerManager.Health <= 0)
            {
                Debug.Log("Player Dead");
                return;
            }
            playerManager.Health -= damageAmount;
            SetHealth(playerManager.Health);
        }

        public void SetHealth(float health)
        {
            var calculateHealth = health / 100f;
            healthHolder.transform.localScale = new Vector3(calculateHealth, 1, 1);
            
        }

        public void SetHealthVisible(bool isGun)
        {
            healthBar.SetActive(isGun);
        }
    }
}