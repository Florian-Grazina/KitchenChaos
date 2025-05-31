using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IKitchenObjectHolder
    {
        public void SetKitchenObject(KitchenObject kitchenObject);
        public KitchenObject GetKitchenObject();
        public void ClearKitchenObject();
        public bool HasKitchenObject();
        public Transform GetKitchenObjectFollowTransform();
    }
}
