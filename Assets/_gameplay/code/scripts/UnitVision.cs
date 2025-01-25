using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    [SerializeField] private float _attackRange = 10f;

    [SerializeField] private bool _hasTarget;
    private float _castRate = 0.2f;
    private float _timer;

    private List<UnitHealth> _targets;
    private UnitHealth _closestTarget;

    public bool HasTargetInAttackRange { get => _hasTarget; }
    public UnitHealth ClosestTarget => _closestTarget;

    public float AttackRange { get => _attackRange; set => _attackRange = value; }

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

        _hasTarget = _closestTarget != null && Vector3.Distance(transform.position, _closestTarget.transform.position) < AttackRange + _closestTarget.TargetSize;
    }

    private UnitHealth FindClosestTarget()
    {
        UnitHealth closest = null;
        float closestDistance = float.MaxValue;

        foreach (UnitHealth target in _targets)
        {
            if (target == null) continue;

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = target;
            }
        }

        return closest;
    }
}