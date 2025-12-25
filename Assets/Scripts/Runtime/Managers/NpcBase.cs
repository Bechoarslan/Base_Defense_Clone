using System;
using System.Collections;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects.NpcData;
using Runtime.Enums.NpcType;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class NpcBase : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private NpcType npcType;
        [SerializeField] protected CD_NpcData npcData;
        
        #endregion

        #region Private Variables

       
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
        
        #region Hostage and Ammo Npc Common Methods

        protected IEnumerator MoveLoop(Transform firstPos,Transform secondPos)
        {
            while (true)
            {

                yield return MoveTo(firstPos.position);
                yield return new WaitForSeconds(npcData.Data.WaitTime);
                yield return MoveTo(secondPos.position);
                yield return new WaitForSeconds(npcData.Data.WaitTime);
            }
        }

        private IEnumerator MoveTo(Vector3 position)
        {
          
            var movePos = new Vector3(position.x, transform.position.y, position.z);
            while (Vector3.Distance(transform.position, movePos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    movePos,
                    npcData.Data.MoveSpeed * Time.deltaTime
                );
                yield return null;
            }
        }

        #endregion
        
    }
}