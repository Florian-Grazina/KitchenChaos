using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform tomatoPrefab;
    [SerializeField] private Transform counterTopPoint;


    public void Interact()
    {
        Debug.Log("Clearing counter...");
        Transform tomato = Instantiate(tomatoPrefab, counterTopPoint);
        tomato.localPosition = Vector3.zero;
    }
}
