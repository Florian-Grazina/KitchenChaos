using Assets.Scripts.Events;
using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    #region serialize fields
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    #endregion

    public event EventHandler<OnProgressChangedEventArgs> OnCuttingProgressChanged;

    #region fields
    private int cuttingProgress;
    #endregion

    #region public methods
    public override void Interact(Player player)
    {
        // counter has no object
        if (!HasKitchenObject())
        {
            // player has an object
            if (player.HasKitchenObject())
            {
                // if the kitchen object on the player can be cut
                if (HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // drop it
                    player.GetKitchenObject().SetKitchenObjectHolder(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO());

                    OnCuttingProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
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
        // counter has an object && player has no object, cut it
        if (HasKitchenObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO()) && !player.HasKitchenObject())
        {
            cuttingProgress++;
            KitchenObjectSO inputKitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
            CuttingRecipeSO cuttingRecipe = GetCuttingRecipe(inputKitchenObjectSO);

            OnCuttingProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipe.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipe.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetCuttingRecipeOutput(inputKitchenObjectSO);
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    #endregion

    #region private methods
    private KitchenObjectSO GetCuttingRecipeOutput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipe(inputKitchenObjectSO);

        if(cuttingRecipe != null)
            return cuttingRecipe.outputKitchenObjectSO;

        Debug.LogWarning($"No cutting recipe found for {inputKitchenObjectSO}");
        return null;
    }

    private bool HasCuttingRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        return cuttingRecipeSOArray.Any(cuttingRecipe => cuttingRecipe.inputKitchenObjectSO == inputKitchenObjectSO);
    }

    private CuttingRecipeSO GetCuttingRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        return cuttingRecipeSOArray.FirstOrDefault(cuttingRecipe => cuttingRecipe.inputKitchenObjectSO == inputKitchenObjectSO);
    }
    #endregion
}
