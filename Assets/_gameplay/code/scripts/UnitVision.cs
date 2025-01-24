using UnityEngine;

public class UnitVision : MonoBehaviour
{
    [SerializeField] private float _attackRange = 10f;

    private bool _targetInRange;
    private float _castRate = 0.2f;
    private float _timer;

    public bool TargetInRange { get => _targetInRange; }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _castRate) return;

        CheckDistance();

        _timer = 0f;
    }

    private void CheckDistance()
    {

    }
}
