using System;
using System.Net.Http.Headers;
using DG.Tweening;
using Unity.VisualScripting;
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
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _weaponParent;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _activeGunIndex = 0;
    [SerializeField] private float _swapTime = 1;
    [SerializeField] private GunSetup[] _gunSteps;
    [SerializeField] private float _chargeTime = 1f;
    [SerializeField] private GunSetup _chargedWeaponData;
    [SerializeField] private RecoilData _recoilData;

    private Tweener _recoilTween;
    private bool _changingWeapons;

    private GunSetup ActiveGun => _gunSteps[_activeGunIndex];
    void Update()
    {
        ChangeWeapons();

        Fire();
    }

    private void ChangeWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !_changingWeapons)
        {
            _changingWeapons = true;

            _weaponParent.localPosition = Vector3.zero;

            _weaponParent.DOLocalMoveY(-5f, _swapTime / 2f).SetEase(Ease.InSine).OnComplete(() =>
            {
                ActiveGun.GunObject.SetActive(false);

                _activeGunIndex++;

                if (_activeGunIndex > _gunSteps.Length - 1)
                {
                    _activeGunIndex = 0;
                }

                ActiveGun.GunObject.SetActive(true);

                _weaponParent.DOLocalMoveY(0, _swapTime / 2f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    _changingWeapons = false;
                });
            });
        }
    }

    float _chargeTimer;

    void Fire()
    {
        if (_changingWeapons) return;

        if (Input.GetMouseButton(0))
        {
            _chargeTimer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _layer))
            {
                Vector3 worldPosition = hit.point;
                ShootProjectile(worldPosition, _activeGunIndex == 0 && _chargeTimer >= _chargeTime ? _chargedWeaponData : ActiveGun);
            }
            else
            {
                Vector3 fallbackPosition = ray.origin + ray.direction * _rayDistance;
                ShootProjectile(fallbackPosition, _activeGunIndex == 0 && _chargeTimer >= _chargeTime ? _chargedWeaponData : ActiveGun);
            }

            _chargeTimer = 0;
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
