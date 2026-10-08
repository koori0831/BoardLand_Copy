using Alchemy.Inspector;
using UnityEngine;

public class MainCameraBillboardObject : MonoBehaviour
{
    private Transform mainCam;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (mainCam == null) return;

        transform.LookAt(transform.position + mainCam.rotation * Vector3.forward, mainCam.rotation * Vector3.up);
    }

    [Button]
    private void LookAtCamera()
    {
        mainCam = Camera.main.transform;
        transform.LookAt(transform.position + mainCam.rotation * Vector3.forward, mainCam.rotation * Vector3.up);
    }
}
