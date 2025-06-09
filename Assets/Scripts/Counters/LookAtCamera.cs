using UnityEngine;

public class LookAtCalera : MonoBehaviour
{
    protected void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
