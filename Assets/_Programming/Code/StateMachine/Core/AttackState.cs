using System;
using UnityEngine;

namespace ProjectGZA
{
    public class AttackStateBasic : BaseState
    {
        public AttackStateBasic(Enemy enemy, UnitVision vision)
        {
            _enemy = enemy;
            _vision = vision;
            _attackRate = enemy.AttackRate;
        }

        protected Enemy _enemy;
        protected UnitVision _vision;
        protected float _attackRate;
        protected float _timer;

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {
            _timer = 0;
        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer < _attackRate) return;

            PerformAttack();

            _timer = 0;
        }

        protected virtual void PerformAttack()
        {

        }
    }
}
