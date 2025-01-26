using UnityEngine;
using UnityEngine.VFX;

public class TankShield : MonoBehaviour
{
    [SerializeField] private Transform _shield;
    public float sphereRadius = 1.0f;
    public float maxDistance = 10.0f;
    public LayerMask detectionLayer;

    void Update()
    {
        Vector3 origin = transform.position;

        var hits = Physics.OverlapSphere(origin, sphereRadius, detectionLayer);

        _shield.gameObject.SetActive(!(hits.Length > 0));
    }
}
