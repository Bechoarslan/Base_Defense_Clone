using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers
{
    public class GemHouseController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform fenceRight;
        [SerializeField] private Transform fenceLeft;
        [SerializeField] private Transform cart;
        [SerializeField] private TextMeshPro gemCountText;

        #endregion

        #region Private Variables
        private Vector3 _fenceRightOpenPos = new Vector3(0, -90, 0);
        private Vector3 _fenceLeftOpenPos = new Vector3(0, 90, 0);
        private int _gemCounts = 8;

        #endregion

        #endregion

        private void Start()
        {
            SetReadyGems();
            
        }

        private void SetReadyGems()
        {
            gemCountText.text = _gemCounts.ToString();
            fenceRight.DOLocalRotate( _fenceRightOpenPos, 1f);
            fenceLeft.DOLocalRotate( _fenceLeftOpenPos, 1f);
            cart.DOLocalMoveZ(-5f, 1f).OnComplete(() =>
            {
                gemCountText.gameObject.SetActive(true);
            });
        }
        
        
        private void CloseFencesAndCart()
        {
            cart.DOLocalMoveZ(-5F, 1f).OnComplete(() =>
            {
                fenceRight.DOLocalRotate( _fenceRightOpenPos, 1f);
                fenceLeft.DOLocalRotate( _fenceLeftOpenPos, 1f);
            });
        }
    }
}