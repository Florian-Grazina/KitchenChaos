using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private float plateOffsetY = 0.2f;

    private List<Transform> plateVisualGameObjectList;

    protected void Awake()
    {
        plateVisualGameObjectList = new();
    }

    protected void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * plateVisualGameObjectList.Count, 0f);
        plateVisualGameObjectList.Add(plateVisualTransform);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        if (plateVisualGameObjectList.Count > 0)
        {
            Transform plateVisualTransform = plateVisualGameObjectList[^1];
            plateVisualGameObjectList.Remove(plateVisualTransform);
            Destroy(plateVisualTransform.gameObject);
        }
    }
}
