using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

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
                GetKitchenObject().DestroySelf();

            }
        }
    }
}
