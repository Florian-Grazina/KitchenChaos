using Assets.Scripts.Interfaces;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectHolder
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject _kitchenObject;

    public void Interact(Player player)
    {
        if (_kitchenObject == null)
            SpawnKitchenObject();
        else
        {
            _kitchenObject.SetObjectHolder(player);
        }
    }

    #region IKitchenObjectHolder
    public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;

    public KitchenObject GetKitchenObject() => _kitchenObject;

    public void ClearKitchenObject() => _kitchenObject = null;

    public bool HasKitchenObject() => _kitchenObject != null;

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
    #endregion

    private void SpawnKitchenObject()
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetObjectHolder(this);
    }
}
