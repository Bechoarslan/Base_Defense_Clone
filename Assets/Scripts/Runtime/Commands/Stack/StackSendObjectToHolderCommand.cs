using DG.Tweening;
using Runtime.Data.UnityObjects;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class StackSendObjectToHolderCommand
    {
        public void Execute(GameObject stackObj, Transform areaHolder, Transform stackHolder, CD_StackData ammoData)
        {
            stackObj.transform.SetParent(areaHolder);
            stackObj.transform.localPosition = Vector3.zero;
            stackObj.transform.localRotation = Quaternion.identity;
            stackObj.SetActive(true);
       
            stackObj.transform.DOLocalJump(
                new Vector3(0, 2f, 0),
                2f, 1, 0.3f).OnComplete(() =>
            {
                stackObj.transform.SetParent(stackHolder);
                var childCount = stackHolder.childCount;
                var newPosition = new Vector3(0, ammoData.stackData.StackHolderOffset.y * childCount, 0);
                stackObj.transform.DOLocalMove(newPosition, 0.5f);
                stackObj.transform.localRotation = Quaternion.identity;
            });
           
        }
    }
}