using ProjectGZA;
using ProjectGZA.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitHealth))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private UnitHealth _unitHealth;
    private UnitVision _unitVision;
    private StateMachine _stateMachine;

    private void Awake()
    {
        InitializeComponents();
    }

    void Start()
    {
        InitializeStateMachine();
    }

    private void InitializeComponents()
    {
        _agent = GetComponent<NavMeshAgent>();
        _unitHealth = GetComponent<UnitHealth>();
        _unitVision = GetComponent<UnitVision>();
    }

    private void InitializeStateMachine()
    {
        _stateMachine = new();

        var walkState = new WalkState(_agent, _unitVision);
        var attackState = new AttackState();
        var deathState = new DeathState();

        _stateMachine.AddTransition(walkState, attackState, new FuncPredicate(() => _unitVision.HasTarget));
        _stateMachine.AddTransition(attackState, walkState, new FuncPredicate(() => !_unitVision.HasTarget));
        _stateMachine.AddAnyTransition(deathState, new FuncPredicate(() => _unitHealth.IsDead));

        _stateMachine.SetState(walkState);
    }
}
