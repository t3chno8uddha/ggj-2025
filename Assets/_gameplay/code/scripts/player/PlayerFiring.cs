using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    bool isFiring = false;

    float fireTime;

    [SerializeField] ParticleSystem[] weaponSystem;

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
        isFiring = false;
        var releaseTime = Time.time - fireTime;

        switch (releaseTime)
        {
            case < 0.25f:
                weaponSystem[0].Play();
                break;
            case < 0.5f:
                weaponSystem[1].Play();
                break;
            case > 0.5f:
                weaponSystem[2].Play();
                break;
        }

        fireTime = 0;
    }


}