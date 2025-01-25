using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    [SerializeField] private float _attackRange = 10f;

    private bool _hasTarget;
    private float _castRate = 0.2f;
    private float _timer;

    private List<UnitHealth> _targets;
    private UnitHealth _closestTarget;

    public bool HasTargetInAttackRange { get => _hasTarget; }
    public UnitHealth ClosestTarget => _closestTarget;

    private void Start()
    {
        _targets = TargetManager.Instance.MainTargets;

        CheckDistance();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _castRate) return;

        CheckDistance();

        _timer = 0f;
    }

    private void CheckDistance()
    {
        _closestTarget = FindClosestTarget();

        _hasTarget = (transform.position - _closestTarget.transform.position).sqrMagnitude < _attackRange;
    }

    private UnitHealth FindClosestTarget()
    {
        return _targets[0];
    }
}