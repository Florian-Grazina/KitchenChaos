using Assets.Scripts.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image progressBar;

    private IHasProgress hasProgress;

    protected void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError($"IHasProgress component not found on the specified GameObject : {hasProgressGameObject}");
            return;
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, OnProgressChangedEventArgs e)
    {
        float progress = e.progressNormalized;
        progressBar.fillAmount = progress;

        if (progress > 0 && progress < 1)
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
