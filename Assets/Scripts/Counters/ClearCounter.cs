
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
                if (player.GetKitchenObject() is PlateKitchenObject plate)
                {

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
