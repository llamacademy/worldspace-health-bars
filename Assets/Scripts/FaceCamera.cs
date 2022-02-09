using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera Camera;

    private void Update()
    {
        transform.LookAt(Camera.transform, Vector3.up);
    }
}
