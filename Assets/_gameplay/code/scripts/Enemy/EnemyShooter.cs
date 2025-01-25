using ProjectGZA;
using ProjectGZA.StateMachine;
using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;

    public Projectile ProjectilePrefab { get => _projectilePrefab; set => _projectilePrefab = value; }
    public Transform ProjectileSpawnPoint { get => _projectileSpawnPoint; set => _projectileSpawnPoint = value; }

    protected override void InitializeStateMachine()
    {
        base.InitializeStateMachine();

        var walkState = new WalkState(_agent, _unitVision);
        var attackState = new AttackStateShooter(this, _unitVision);
        var deathState = new DeathState(_agent);

        _stateMachine.AddTransition(walkState, attackState, new FuncPredicate(() => _unitVision.HasTargetInAttackRange));
        _stateMachine.AddTransition(attackState, walkState, new FuncPredicate(() => !_unitVision.HasTargetInAttackRange));
        _stateMachine.AddAnyTransition(deathState, new FuncPredicate(() => _unitHealth.IsDead));

        _stateMachine.SetState(walkState);
    }
}
