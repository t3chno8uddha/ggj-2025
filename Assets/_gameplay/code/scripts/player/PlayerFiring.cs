using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class GunSetup
{
    public GameObject GunObject;
    public Transform ShootPoint;
    public Projectile ProjectilePrefab;
    public int Damage;
    public int BulletAmount = 1;
    public int SpreadAngle = 10;
}

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 100f;
    [SerializeField] private int activeGunIndex = 0;
    [SerializeField] private GunSetup[] _gunSteps;

    private GunSetup ActiveGun => _gunSteps[activeGunIndex];
    bool isFiring = false;
    float fireTime;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CountFire();
        }

        Fire();
    }

    void CountFire()
    {
        isFiring = true;
        fireTime = Time.time;
    }

    void Fire()
    {
        if (Input.GetMouseButtonUp(0)) // Check for left mouse button press
        {
            // Calculate the screen center point
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

            // Create a ray from the camera through the screen center
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            // Perform the raycast
            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance))
            {
                // If we hit an object, get the world position of the hit
                Vector3 worldPosition = hit.point;
                Debug.Log($"Hit at: {worldPosition}");

                ShootProjectile(worldPosition, ActiveGun);
                // Shoot projectile towards the hit point
            }
            else
            {
                // If no object is hit, calculate a point in the direction of the ray
                Vector3 fallbackPosition = ray.origin + ray.direction * _rayDistance;
                Debug.Log($"No hit detected. Shooting towards: {fallbackPosition}");
                ShootProjectile(fallbackPosition, ActiveGun);
                // Shoot projectile towards this fallback position
            }
        }
    }

    private void ShootProjectile(Vector3 targetPosition, GunSetup gunData)
    {
        for (int i = 0; i < gunData.BulletAmount; i++)
        {
            var projectile = MonoBehaviour.Instantiate(gunData.ProjectilePrefab, gunData.ShootPoint.position, Quaternion.identity, null);
            var shiftedPositions = GetSpreadDirection(targetPosition, gunData.SpreadAngle);
            projectile.SetTarget(shiftedPositions, gunData.Damage);
        }
    }

    Vector3 GetSpreadDirection(Vector3 forward, int maxSpreadAngle)
    {
        // Generate random angles for the spread
        float randomYaw = Random.Range(-maxSpreadAngle, maxSpreadAngle);
        float randomPitch = Random.Range(-maxSpreadAngle, maxSpreadAngle);

        // Create a rotation based on the random angles
        Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);

        // Apply the rotation to the forward direction
        return spreadRotation * forward;
    }

}