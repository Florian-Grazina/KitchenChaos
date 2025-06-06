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
        Hide();
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, OnProgressChangedEventArgs e)
    {
        float cuttingProgress = e.progressNormalized;
        progressBar.fillAmount = cuttingProgress;

        if (cuttingProgress > 0 && cuttingProgress < 1)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        progressBar.fillAmount = 0f;
    }
}
