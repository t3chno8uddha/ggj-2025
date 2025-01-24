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

    public bool HasTarget { get => _hasTarget; }
    public UnitHealth ClosestTarget { get => _closestTarget; set => _closestTarget = value; }

    private void Start()
    {
        _targets = TargetManager.Instance.MainTargets;
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
        ClosestTarget = FindClosestTarget();
    }

    private UnitHealth FindClosestTarget()
    {
        UnitHealth closestTarget = null;
        float closestDistanceSqr = float.MaxValue;

        for (int i = 0; i < _targets.Count; i++)
        {
            UnitHealth target = _targets[i];
            float distanceSqr = (transform.position - target.transform.position).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestTarget = target;
            }
        }

        return closestTarget;
    }
}