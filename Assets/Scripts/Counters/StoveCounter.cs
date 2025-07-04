using System.Linq;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    #region serialize fields
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private float fryingTimer;
    private FryingRecipeSO _currentFryingRecipeSO;
    #endregion

    #region unity methods
    protected void Update()
    {
        if (HasKitchenObject())
        {

            if (_currentFryingRecipeSO == null)
                return;

            fryingTimer += Time.deltaTime;

            Debug.Log("Frying timer: " + fryingTimer + " / " + _currentFryingRecipeSO.fryingTimerMax);

            if (fryingTimer > _currentFryingRecipeSO.fryingTimerMax)
            {
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(_currentFryingRecipeSO.outputKitchenObjectSO, this);
                fryingTimer = 0f; // Reset the frying timer after cooking
            }
        }
    }
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
                // if the kitchen object on the player can be fried
                if (HasFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // drop it
                    player.GetKitchenObject().SetKitchenObjectHolder(this);
                    _currentFryingRecipeSO = GetFryingRecipe(GetKitchenObject().GetKitchenObjectSO());
                }
            }
        }
        // counter has an object
        else
        {
            //player has no object, pick it up
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectHolder(player);
                _currentFryingRecipeSO = null;
                fryingTimer = 0f;
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
    }
    #endregion

    #region private methods
    private KitchenObjectSO GetFryingRecipeOutput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipe = GetFryingRecipe(inputKitchenObjectSO);

        if (fryingRecipe != null)
            return fryingRecipe.outputKitchenObjectSO;

        Debug.LogWarning($"No cutting recipe found for {inputKitchenObjectSO}");
        return null;
    }

    private bool HasFryingRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        return fryingRecipeSOArray.Any(fryingRecipe => fryingRecipe.inputKitchenObjectSO == inputKitchenObjectSO);
    }
    private FryingRecipeSO GetFryingRecipe(KitchenObjectSO inputKitchenObjectSO)

    {
        return fryingRecipeSOArray.FirstOrDefault(cuttingRecipe => cuttingRecipe.inputKitchenObjectSO == inputKitchenObjectSO);
    }
    #endregion
}
