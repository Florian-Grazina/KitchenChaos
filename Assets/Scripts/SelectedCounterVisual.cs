using Assets.Scripts.Events;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    protected void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        if (visualGameObject != null)
            visualGameObject.SetActive(true);
    }
    private void Hide()
    {
        if (visualGameObject != null)
            visualGameObject.SetActive(false);
    }
}
