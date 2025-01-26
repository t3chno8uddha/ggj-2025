using UnityEngine;

public enum TargetType
{
    Enemy, MainTarget, Obstacle
}

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _projectileSpeed = 10f;
    [SerializeField] protected TargetType _checkTargetType;

    [SerializeField] protected ParticleSystem _particle;
    protected Vector3 _targetPosition;
    protected int _damageToDeal;

    protected float _checkThreshhold = 0.1f;

    [SerializeField] GameObject[] renderers;

    void Start()
    {
        if (renderers.Length > 1)
        {
            int randomRenderer = Random.Range(0, renderers.Length);
            foreach (var _renderer in renderers)
            {
                if (_renderer != renderers[randomRenderer])
                {
                    _renderer.SetActive(false);
                }
            }
        }
    }

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
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<IDamagable>().GetDamage(_damageToDeal);

                DestroyProjectile();
            }
        }
        else if (_checkTargetType == TargetType.MainTarget)
        {
            if (other.gameObject.CompareTag("MainTarget"))
            {
                other.gameObject.GetComponent<IDamagable>().GetDamage(_damageToDeal);

                DestroyProjectile();
            }
        }
    }
    protected virtual void DestroyProjectile()
    {
        if (_particle != null)
        {
            Instantiate(_particle, transform.position, Quaternion.identity, null);
        }

        Destroy(gameObject);
    }
}
