using UnityEngine;

public class WallProjectile : Projectile
{
    [SerializeField] private TemporaryWall _wallPrefab;
    protected override void DestroyProjectile()
    {
        Instantiate(_wallPrefab, transform.position, Quaternion.identity);

        base.DestroyProjectile();
    }
}
