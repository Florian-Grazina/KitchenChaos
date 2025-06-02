using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler ContainerCounter_OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
            SpawnKitchenObject(player);
    }

    private void SpawnKitchenObject(Player player)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectHolder(player);

        ContainerCounter_OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
