using System;

namespace ProjectGZA
{
    public class AttackState : BaseState
    {
        public AttackState(Enemy enemy, UnitVision vision)
        {
            _enemy = enemy;
            _vision = vision;
            _attackRate = enemy.AttackRate;
        }

        private Enemy _enemy;
        private UnitVision _vision;
        private float _attackRate;
        private float _timer;
        public override void OnEnter()
        {

        }

        public override void OnExit()
        {
            _timer = 0;
        }

        public override void Update()
        {
            _timer += _attackRate;

            if (_timer < _attackRate) return;

            _vision.ClosestTarget.GetDamage(_enemy.Damage);

            _timer = 0;
        }
    }
}
