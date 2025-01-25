using UnityEngine;

public enum TargetType
{
    Enemy, MainTarget
}
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private TargetType _checkTargetType;
    private Vector3 _targetPosition;
    private int _damageToDeal;

    private float _checkThreshhold = 0.1f;

    private void Update()
    {
        var shiftedPosition = Vector3.MoveTowards(transform.position, _targetPosition, _projectileSpeed);
        transform.position = shiftedPosition;

        if (Vector3.Distance(transform.position, _targetPosition) < _checkThreshhold)
        {
            DestroyProjectile();
        }
    }

    public void SetTarget(Vector3 targetPosition, int damageToDeal)
    {
        _targetPosition = targetPosition;
        _damageToDeal = damageToDeal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_checkTargetType == TargetType.Enemy)
        {
            if (other.gameObject.CompareTag("MainTarget"))
            {
                other.gameObject.GetComponent<IDamagable>().GetDamage(_damageToDeal);

                DestroyProjectile();
            }
        }
        else if (_checkTargetType == TargetType.MainTarget)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<IDamagable>().GetDamage(_damageToDeal);

                DestroyProjectile();
            }
        }

    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
