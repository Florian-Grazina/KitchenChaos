using Assets.Scripts.Interfaces;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectHolder _kitchenObjectHolder;

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;

    public IKitchenObjectHolder GetKitchenObjectHolder() => _kitchenObjectHolder;

    public void SetKitchenObjectHolder(IKitchenObjectHolder kitchenObjectHolder)
    {
        if (_kitchenObjectHolder != null)
            _kitchenObjectHolder.ClearKitchenObject();

        _kitchenObjectHolder = kitchenObjectHolder;

        if (_kitchenObjectHolder.HasKitchenObject())
            Debug.LogError($"KitchenObject is already set on this {kitchenObjectHolder.GetType()}!");

        _kitchenObjectHolder.SetKitchenObject(this);

        transform.parent = kitchenObjectHolder.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        if (_kitchenObjectHolder != null)
            _kitchenObjectHolder.ClearKitchenObject();

        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectHolder kitchenObjectHolder)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectHolder(kitchenObjectHolder);

        return kitchenObject;
    }
}
