using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator myAnimator;

    [SerializeField] private ContainerCounter containerCounter;

    private const string OPEN_CLOSE = "OpenClose";

    protected void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    protected void Start()
    {
        containerCounter.ContainerCounter_OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        if (myAnimator != null)
            myAnimator.SetTrigger(OPEN_CLOSE);
    }
}
