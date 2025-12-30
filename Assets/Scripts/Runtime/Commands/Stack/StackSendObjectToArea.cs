using DG.Tweening;
using Runtime.Data.UnityObjects;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackSendObjectToArea
    {
        public void Execute(GameObject stackObj, Transform areaHolder, Transform stackHolder, CD_StackData ammoData)
        {
            
            stackObj.transform.DOLocalJump(new Vector3(0,2f,0) ,0,1,0.3f).OnComplete(() =>
            {
             
                var childCount = areaHolder.childCount;
                var offSetX = (childCount % 2) ;
                var offSetZ = ((childCount / 2) % 2) ;
                var offSetY = (childCount / 4) ;
                stackObj.transform.SetParent(areaHolder);
                stackObj.transform.DOLocalMove(new Vector3(offSetX + ammoData.stackData.StackAreaOffset.x, 
                    offSetY * ammoData.stackData.StackAreaOffset.y  , offSetZ - ammoData.stackData.StackAreaOffset.y), 0.2f);
                stackObj.transform.localRotation = Quaternion.identity;
            });
        }
    }
}