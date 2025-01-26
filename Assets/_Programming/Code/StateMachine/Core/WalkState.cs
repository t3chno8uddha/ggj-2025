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

            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(_agent.transform.position, closestEnemy.transform.position, NavMesh.AllAreas, path))
            {
                if (path.status == NavMeshPathStatus.PathInvalid)
                {
                    _agent.SetDestination(_agent.transform.position);
                }
            }

            _agent.stoppingDistance = _unitVision.AttackRange + closestEnemy.TargetSize;

            Vector3 directionToTarget = closestEnemy.transform.position - _agent.transform.position;

            RaycastHit hit;
            if (Physics.Raycast(_agent.transform.position.WithY(2f), directionToTarget.normalized, out hit, 100))
            {

                if (hit.transform != closestEnemy.transform)
                {
                    _agent.stoppingDistance = 1f;

                    _unitVision.CanSee = false;
                }
                else
                {
                    _unitVision.CanSee = true;
                    Debug.DrawRay(_agent.transform.position.WithY(2f), directionToTarget.normalized * 100, Color.red, 2f);
                }
            }
            else
            {
                _unitVision.CanSee = false;
            }

            _agent.SetDestination(closestEnemy.transform.position);
        }
    }
}
