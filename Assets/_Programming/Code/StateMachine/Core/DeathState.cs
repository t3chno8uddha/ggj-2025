using UnityEngine.AI;
using UnityEngine;

namespace ProjectGZA
{
    public class DeathState : BaseState
    {
        public DeathState(NavMeshAgent navMesh)
        {
            _agent = navMesh;
        }

        private NavMeshAgent _agent;
        public override void OnEnter()
        {
            MonoBehaviour.Destroy(_agent.gameObject);
        }

        public override void OnExit()
        {

        }

        public override void Update()
        {

        }
    }
}
