using System;
using System.Linq;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public enum StateEnum
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    #region serialize fields
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    #endregion

    #region fields
    private float fryingTimer;
    private FryingRecipeSO currentFryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO currentBurningRecipeSO;
    private StateEnum state;
    private StateEnum State
    {
        get => state;
        set
        {
            if (state == value) return;
            state = value;
            OnStateChanged?.Invoke(this, new OnStatechangedEventArgs() { state = state });
        }
    }
    #endregion

    #region events
    public EventHandler<OnStatechangedEventArgs> OnStateChanged;
    public class OnStatechangedEventArgs : EventArgs { public StateEnum state; }
    #endregion

    #region unity methods
    protected void Start()
    {
        State = StateEnum.Idle;
    }

    protected void Update()
    {
        if (!HasKitchenObject())
            return;

        switch (State)
        {
            case StateEnum.Idle:
                // HandleIdleState();
                break;
            case StateEnum.Frying:
                HandleFryingState();
                break;
            case StateEnum.Fried:
                HandleFriedState();
                break;
            case StateEnum.Burned:
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

            State = StateEnum.Fried;
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

            State = StateEnum.Burned;
            OnStateChanged?.Invoke(this, new OnStatechangedEventArgs() { state = state });
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
                    State = StateEnum.Frying;
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
                State = StateEnum.Idle;
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
