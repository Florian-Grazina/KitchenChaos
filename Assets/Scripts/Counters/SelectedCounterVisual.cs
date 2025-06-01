using Assets.Scripts.Events;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObject;

    protected void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        foreach (var item in visualGameObject)
        {
            if (item != null)
                item.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (var item in visualGameObject)
        {
            if (item != null)
                item.SetActive(false);
        }
    }
}
