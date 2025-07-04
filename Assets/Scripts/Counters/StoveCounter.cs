using System.Linq;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    #region serialize fields
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private FryingRecipeSO currentFryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO currentBurningRecipeSO;
    private State state;
    #endregion

    #region unity methods
    protected void Start()
    {
        state = State.Idle;
    }

    protected void Update()
    {
        if (!HasKitchenObject())
            return;

        switch (state)
        {
            case State.Idle:
                // HandleIdleState();
                break;
            case State.Frying:
                HandleFryingState();
                break;
            case State.Fried:
                HandleFriedState();
                break;
            case State.Burned:
                // HandleBurnedState();
                break;
        }

    }
    #endregion

    #region state methods
    private void HandleFryingState()
    {
        if (currentFryingRecipeSO == null)
            return;

        fryingTimer += Time.deltaTime;

        if (fryingTimer > currentFryingRecipeSO.fryingTimerMax)
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(currentFryingRecipeSO.outputKitchenObjectSO, this);

            state = State.Fried;
            currentBurningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            burningTimer = 0f;
        }
    }

    private void HandleFriedState()
    {
        if (currentFryingRecipeSO == null)
            return;

        burningTimer += Time.deltaTime;

        Debug.Log($"Burning timer: {burningTimer} / {currentBurningRecipeSO.burningTimerMax}");

        if (burningTimer > currentBurningRecipeSO.burningTimerMax)
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(currentBurningRecipeSO.outputKitchenObjectSO, this);

            state = State.Burned;
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
                    currentFryingRecipeSO = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    fryingTimer = 0f;
                    state = State.Frying;
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
                state = State.Idle;
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
    }
    #endregion

    #region private methods
    private bool HasFryingRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        return fryingRecipeSOArray.Any(fryingRecipe => fryingRecipe.inputKitchenObjectSO == inputKitchenObjectSO);
    }

    private FryingRecipeSO GetFryingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        return fryingRecipeSOArray.FirstOrDefault(cuttingRecipe 
            => cuttingRecipe.inputKitchenObjectSO == inputKitchenObjectSO);
    }

    private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        return burningRecipeSOArray.FirstOrDefault(cuttingRecipe 
            => cuttingRecipe.inputKitchenObjectSO == inputKitchenObjectSO);
    }
    #endregion
}
