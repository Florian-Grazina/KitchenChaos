using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        // counter has no object
        if (!HasKitchenObject())
        {
            // player has an object, drop it
            if (player.HasKitchenObject())
                player.GetKitchenObject().SetKitchenObjectHolder(this);
        }
        // counter has an object
        else
        {
            //player has no object, pick it up
            if (!player.HasKitchenObject())
                GetKitchenObject().SetKitchenObjectHolder(player);
        }
    }

    public override void InteractAlternate(Player player)
    {
        // counter has an object
        if (HasKitchenObject())
        {
            //player has no object, cut it
            if (!player.HasKitchenObject())
            {
                KitchenObjectSO cutKitchenObjectSO = GetCuttingRecipeOutput(GetKitchenObject().GetKitchenObjectSO());

                if(cutKitchenObjectSO != null)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
                }
            }
        }
    }

    private KitchenObjectSO GetCuttingRecipeOutput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipeSOArray)
        {
            if (cuttingRecipe.inputKitchenObjectSO == inputKitchenObjectSO)
                return cuttingRecipe.outputKitchenObjectSO;
        }
        Debug.LogWarning($"No cutting recipe found for {inputKitchenObjectSO}");
        return null;
    }
}
