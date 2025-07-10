using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField]
    private List<KitchenObjectSO> valideKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    protected void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
}
