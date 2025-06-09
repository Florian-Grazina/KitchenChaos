using UnityEngine;

public class LookAtCalera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted
    }

    [SerializeField] private Mode mode;

    protected void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;

        }
    }
}
