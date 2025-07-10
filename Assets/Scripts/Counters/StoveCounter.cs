using Assets.Scripts.Events;
using System;
using System.Linq;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
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
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs(state));

            if(State == StateEnum.Idle)
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs(0f));
        }
    }
    #endregion

    #region events
    public EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
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
        ProgressChanged(fryingTimer, currentFryingRecipeSO.fryingTimerMax);

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
        ProgressChanged(burningTimer, currentBurningRecipeSO.burningTimerMax);

        if (burningTimer > currentBurningRecipeSO.burningTimerMax)
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(currentBurningRecipeSO.outputKitchenObjectSO, this);

            State = StateEnum.Burned;
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
            // player has an object
            if (player.HasKitchenObject())
            {
                // player has a plate
                if (player.GetKitchenObject() is PlateKitchenObject plate)
                {
                    // the ingredient can be added to the plate
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        State = StateEnum.Idle;
                    }
                }
            }

            //player has no object, pick it up
            else
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

    private void ProgressChanged(float timer, float maxTimer)
    {
        float progress = timer / maxTimer;
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs(progress));
    }
    #endregion
}
