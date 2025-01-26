using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    [SerializeField] private float _attackRange = 10f;

    [SerializeField] private bool _hasTarget;
    [SerializeField] private bool _canSee;
    private float _castRate = 0.2f;
    private float _timer;

    private List<UnitHealth> _targets;
    private UnitHealth _closestTarget;


    public UnitHealth ClosestTarget => _closestTarget;

    public float AttackRange { get => _attackRange; set => _attackRange = value; }
    public bool HasTargetInAttackRange { get => _hasTarget; set => _hasTarget = value; }
    public bool CanSee { get => _canSee; set => _canSee = value; }

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

        _hasTarget = _closestTarget != null && _canSee && Vector3.Distance(transform.position, _closestTarget.transform.position) < AttackRange + _closestTarget.TargetSize;
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