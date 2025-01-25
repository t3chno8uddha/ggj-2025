using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] float smooth;
    [SerializeField] float multiplier;

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * multiplier;
        float mouseY = Input.GetAxis("Mouse Y") * multiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}