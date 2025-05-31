using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter _clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;

    public ClearCounter GetClearCounter() => _clearCounter;

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (_clearCounter != null)
            _clearCounter.ClearKitchenObject();

        _clearCounter = clearCounter;

        if(_clearCounter.HasKitchenObject())
            Debug.LogError("KitchenObject is already set on this ClearCounter!");

        _clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
}
