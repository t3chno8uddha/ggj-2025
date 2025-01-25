using UnityEngine;

namespace ProjectGZA
{
    public class AttackStateShooter : AttackStateBasic
    {
        public AttackStateShooter(EnemyShooter enemy, UnitVision vision) : base(enemy, vision)
        {
            _projectilePrefab = enemy.ProjectilePrefab;
            _projectileSpawnPoint = enemy.ProjectileSpawnPoint;
        }

        private Projectile _projectilePrefab;
        private Transform _projectileSpawnPoint;

        protected override void PerformAttack()
        {
            if (_vision.ClosestTarget == null) return;

            var targetPosition = _vision.ClosestTarget.transform.position;
            var spawnPoint = _projectileSpawnPoint.position;
            var projectile = MonoBehaviour.Instantiate(_projectilePrefab, spawnPoint, Quaternion.identity, null);
            projectile.SetTarget(targetPosition.WithY(targetPosition.y + 1.5f), _enemy.Damage);
        }
    }
}
