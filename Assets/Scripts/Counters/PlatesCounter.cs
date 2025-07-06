using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    #region fields
    private float spawnPlateTimer;
    private int platesSpawnAmount;
    private int platesSpawnMax = 4;
    #endregion

    #region serialized fields
    [SerializeField] private float spawnPlatetimerMax = 4f;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    #endregion

    #region events
    public event EventHandler OnPlateSpawned;
    #endregion

    #region unity methods
    protected void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= spawnPlatetimerMax)
        {
            spawnPlateTimer = 0f;
            if (platesSpawnAmount < platesSpawnMax)
            {
                platesSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    #endregion

    public override void Interact(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void InteractAlternate(Player player)
    {
        throw new System.NotImplementedException();
    }
}
