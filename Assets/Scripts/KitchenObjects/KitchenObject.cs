using Assets.Scripts.Interfaces;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectHolder _kitchenObjectHolder;

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;

    public IKitchenObjectHolder GetKitchenObjectHolder() => _kitchenObjectHolder;

    public void SetObjectHolder(IKitchenObjectHolder kitchenObjectHolder)
    {
        if (_kitchenObjectHolder != null)
            _kitchenObjectHolder.ClearKitchenObject();

        _kitchenObjectHolder = kitchenObjectHolder;

        if(_kitchenObjectHolder.HasKitchenObject())
            Debug.LogError($"KitchenObject is already set on this {kitchenObjectHolder.GetType()}!");

        _kitchenObjectHolder.SetKitchenObject(this);

        transform.parent = kitchenObjectHolder.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
}
