using Assets.Scripts.Events;
using System;
using UnityEngine;
using static StoveCounter;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject stoveParticleGameObject;

    protected void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StateEnum.Frying || e.state == StateEnum.Fried;
        stoveOnGameObject.SetActive(showVisual);
        stoveParticleGameObject.SetActive(showVisual);
    }
}
