using Assets.Scripts.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private Image background;
    [SerializeField] private CuttingCounter cuttingCounter;

    protected void Start()
    {
       cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;
        progressBar.fillAmount = 0f;
        background.enabled = false;
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, OnProgressChangedEventArgs e)
    {
        float cuttingProgress = e.progressNormalized;

        Debug.Log(cuttingProgress);

        if (cuttingProgress > 0)
            background.enabled = true;

        progressBar.fillAmount = cuttingProgress;

        if(cuttingProgress >= 1f)
        {
            background.enabled = false;
            progressBar.fillAmount = 0f;
            cuttingCounter.OnCuttingProgressChanged -= CuttingCounter_OnCuttingProgressChanged;
        }
    }
}
