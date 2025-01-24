using System;
using UnityEngine.AI;

namespace ProjectGZA
{
    public class WalkState : BaseState
    {
        public WalkState(NavMeshAgent agent, UnitVision unitVision)
        {
            _agent = agent;
            _unitVision = unitVision;
        }

        private NavMeshAgent _agent;
        private UnitVision _unitVision;

        public override void OnEnter()
        {
            throw new NotImplementedException();
        }

        public override void OnExit()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            var closestEnemy = _unitVision.ClosestTarget;
            _agent.SetDestination(closestEnemy.transform.position);
        }
    }
}
