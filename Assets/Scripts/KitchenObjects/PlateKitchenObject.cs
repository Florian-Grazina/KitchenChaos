using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    #region field
    [SerializeField]
    private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;
    #endregion

    #region events
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    #endregion

    #region unity methods   
    protected void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    #endregion

    #region public methods
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            Debug.LogError($"KitchenObjectSO {kitchenObjectSO.objectName} is not valid for this plate!");
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            Debug.LogError($"KitchenObjectSO {kitchenObjectSO.objectName} is already on this plate!");
            return false;
        }

        kitchenObjectSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { KitchenObjectSO = kitchenObjectSO });
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() => kitchenObjectSOList;
    #endregion
}
