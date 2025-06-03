using Assets.Scripts.Interfaces;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectHolder
{
    [SerializeField] protected Transform counterTopPoint;
    private KitchenObject _kitchenObject;

    public abstract void Interact(Player player);
    public abstract void InteractAlternate(Player player);

    #region IKitchenObjectHolder
    public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;

    public KitchenObject GetKitchenObject() => _kitchenObject;

    public void ClearKitchenObject() => _kitchenObject = null;

    public bool HasKitchenObject() => _kitchenObject != null;

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
    #endregion

}
