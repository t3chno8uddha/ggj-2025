using UnityEngine;

public class LookAtCameraNEW : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform CameraTransform
    {
        get
        {
            if (_cameraTransform == null)
            {
                _cameraTransform = Camera.main.transform;
            }

            return _cameraTransform;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(CameraTransform.position);
    }
}

