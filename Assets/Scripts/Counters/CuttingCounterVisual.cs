using Assets.Scripts.Events;
using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator myAnimator;

    [SerializeField] private CuttingCounter cuttingCounter;

    private const string CUT = "Cut";

    protected void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    protected void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        if (myAnimator != null)
            myAnimator.SetTrigger(CUT);
    }
}
