using Unity.VisualScripting;

namespace ProjectGZA
{
    public class AttackStateRunner : AttackStateBasic
    {
        public AttackStateRunner(Enemy enemy, UnitVision vision) : base(enemy, vision)
        {

        }

        protected override void PerformAttack()
        {
            _vision.ClosestTarget.GetDamage(_enemy.Damage);
        }
    }
}
