using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler ContainerCounter_OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
            return;

        if (!HasKitchenObject())
            SpawnKitchenObject(player);
    }

    public override void InteractAlternate(Player player)
    {
        // no alternate interaction for container counter
    }

    private void SpawnKitchenObject(Player player)
    {
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        ContainerCounter_OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
