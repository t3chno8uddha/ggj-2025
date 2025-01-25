using ProjectGZA.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(UnitHealth))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _attackRate = 0.5f;
    [SerializeField] protected int _damage = 1;

    protected NavMeshAgent _agent;
    protected UnitHealth _unitHealth;
    protected UnitVision _unitVision;
    protected StateMachine _stateMachine;

    public float AttackRate { get => _attackRate; }
    public int Damage { get => _damage; }

    protected void Awake()
    {
        InitializeComponents();
    }

    protected void Start()
    {
        InitializeStateMachine();
    }

    protected void Update()
    {
        _stateMachine.Update();
    }

    protected void InitializeComponents()
    {
        _agent = GetComponent<NavMeshAgent>();
        _unitHealth = GetComponent<UnitHealth>();
        _unitVision = GetComponent<UnitVision>();

        _agent.stoppingDistance = _unitVision.AttackRange;
    }

    protected virtual void InitializeStateMachine()
    {
        _stateMachine = new();
    }
}
