using UnityEngine;

public class PlatesCounter : BaseCounter
{
    private float spawnPlateTimer;
    [SerializeField] private float spawnPlatetimerMax = 4f;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    protected void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlatetimerMax)
        {
            spawnPlateTimer = 0f;
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
        }
    }

    public override void Interact(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void InteractAlternate(Player player)
    {
        throw new System.NotImplementedException();
    }
}
