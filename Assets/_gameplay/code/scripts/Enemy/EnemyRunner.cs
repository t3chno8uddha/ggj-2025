using ProjectGZA;
using ProjectGZA.StateMachine;

public class EnemyRunner : Enemy
{
    protected override void InitializeStateMachine()
    {
        base.InitializeStateMachine();

        var walkState = new WalkState(_agent, _unitVision);
        var attackState = new AttackStateRunner(this, _unitVision);
        var deathState = new DeathState();

        _stateMachine.AddTransition(walkState, attackState, new FuncPredicate(() => _unitVision.HasTargetInAttackRange));
        _stateMachine.AddTransition(attackState, walkState, new FuncPredicate(() => !_unitVision.HasTargetInAttackRange));
        _stateMachine.AddAnyTransition(deathState, new FuncPredicate(() => _unitHealth.IsDead));

        _stateMachine.SetState(walkState);
    }
}
