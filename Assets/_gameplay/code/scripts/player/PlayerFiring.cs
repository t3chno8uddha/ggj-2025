using System;
using System.Net.Http.Headers;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class GunSetup
{
    public GameObject GunObject;
    public Projectile ProjectilePrefab;
    public int Damage;
    public int BulletAmount = 1;
    public int SpreadAngle = 10;
}

[Serializable]
public class RecoilData
{
    public float RecoilPower = 1;
    public float RecoilDuration = 0.3f;
}

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 100f;
    [SerializeField] private int activeGunIndex = 0;
    [SerializeField] private Transform _weaponParent;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GunSetup[] _gunSteps;
    [SerializeField] private RecoilData _recoilData;

    private Tweener _recoilTween;

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
        if (Input.GetMouseButtonUp(0))
        {

            Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f, 0f);


            Ray ray = Camera.main.ScreenPointToRay(screenCenter);


            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance))
            {
                Vector3 worldPosition = hit.point;
                Debug.Log($"Hit at: {worldPosition}");

                ShootProjectile(worldPosition, ActiveGun);
            }
            else
            {
                Vector3 fallbackPosition = ray.origin + ray.direction * _rayDistance;
                Debug.Log($"No hit detected. Shooting towards: {fallbackPosition}");
                ShootProjectile(fallbackPosition, ActiveGun);
            }
        }
    }

    private void ShootProjectile(Vector3 targetPosition, GunSetup gunData)
    {
        Recoil();

        for (int i = 0; i < gunData.BulletAmount; i++)
        {
            var projectile = MonoBehaviour.Instantiate(gunData.ProjectilePrefab, _spawnPoint.position, Quaternion.identity, null);
            var shiftedPositions = GetSpreadDirection(targetPosition, gunData.SpreadAngle);
            if (i == 0)
            {
                projectile.SetTarget(targetPosition, gunData.Damage);
            }
            else
            {
                projectile.SetTarget(shiftedPositions, gunData.Damage);
            }

        }
    }

    Vector3 GetSpreadDirection(Vector3 forward, int maxSpreadAngle)
    {
        float randomYaw = Random.Range(-maxSpreadAngle, maxSpreadAngle);
        float randomPitch = Random.Range(-maxSpreadAngle, maxSpreadAngle);

        Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);

        return spreadRotation * forward;
    }

    private void Recoil()
    {
        _recoilTween = _weaponParent.DOLocalMoveZ(-_recoilData.RecoilPower, _recoilData.RecoilDuration).OnComplete(() =>
        {
            _recoilTween = _weaponParent.DOLocalMoveZ(0, _recoilData.RecoilDuration);
        });
    }
}
