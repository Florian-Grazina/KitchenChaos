
public class ClearCounter : BaseCounter
{
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
            // player has an object
            if (player.HasKitchenObject())
            {
                // player has a plate
                if (player.GetKitchenObject() is PlateKitchenObject playerPlate)
                {
                    // the ingredient can be added to the plate
                    if (playerPlate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
                // player has an ingredient
                else
                {
                    // the counter has a plate
                    if(GetKitchenObject() is PlateKitchenObject counterPlate)
                    {
                        // the ingredient can be added to the plate
                        if (counterPlate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            player.GetKitchenObject().DestroySelf();
                    }
                }
            }

            //player has no object, pick it up
            else
                GetKitchenObject().SetKitchenObjectHolder(player);
        }
    }

    public override void InteractAlternate(Player player)
    {
        // no alternate interaction for clear counter
    }
}
