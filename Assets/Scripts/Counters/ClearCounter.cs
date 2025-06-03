
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
            //player has no object, pick it up
            if (!player.HasKitchenObject())
                GetKitchenObject().SetKitchenObjectHolder(player);
        }
    }

    public override void InteractAlternate(Player player)
    {
        // no alternate interaction for clear counter
    }
}
