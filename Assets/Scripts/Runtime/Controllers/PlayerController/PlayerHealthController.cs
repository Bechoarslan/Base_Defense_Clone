using System.Collections;
using Runtime.Managers;
using TMPro;
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
        [SerializeField] private TextMeshPro healthText;
        private float _health;
  

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public void OnTakeDamage(float damageAmount)
        {
            if (_health <= 0)
            {
                Debug.Log("Player Dead");
                return;
            }
            _health -= damageAmount;
            SetHealth(_health);
        }

        public void SetHealth(float health)
        {
            var calculateHealth = health / 100f;
            healthHolder.transform.localScale = new Vector3(calculateHealth, 1, 1);
            healthText.text = ((int)health).ToString();
            
        }

        public void SetHealthVisible(bool isGun)
        {
            healthBar.SetActive(isGun);
        }

        public void OnRegenateHealth()
        {
            if (_health < 100)
            {
                StartCoroutine(RegenateHealth());
            }
        }

        private IEnumerator RegenateHealth()
        {
            if(_health >= 100) yield break;
            _health += 5;
            SetHealth(_health);
            yield return new WaitForSeconds(1f);
        }
    }
}