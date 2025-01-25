using UnityEngine;
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

        }

        public override void OnExit()
        {
            _agent.SetDestination(_agent.transform.position);
        }

        public override void Update()
        {
            var closestEnemy = _unitVision.ClosestTarget;
            _agent.SetDestination(closestEnemy.transform.position);
        }
    }
}
