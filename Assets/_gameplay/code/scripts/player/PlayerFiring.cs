using System;
using UnityEngine;

[Serializable]
public class GunSetups
{
    public GameObject _gunObject;
    public Transform _shootPoint;
}

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 100f;
    [SerializeField] private GunSetups[] _gunSteps;
    bool isFiring = false;
    float fireTime;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CountFire();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }
    }

    void CountFire()
    {
        isFiring = true;
        fireTime = Time.time;
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button press
        {
            // Calculate the screen center point
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            // Create a ray from the camera through the screen center
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            // Perform the raycast
            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance))
            {
                // Log or use the world position of the hit
                Vector3 worldPosition = hit.point;
                Debug.Log($"Hit at: {worldPosition}");

            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }


}