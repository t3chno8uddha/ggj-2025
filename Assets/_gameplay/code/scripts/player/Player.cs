using UnityEngine;

public class Player : Singleton<Player>
{
    // Start is called before the first frame update
    private UnitHealth _unitHealth;
    public Vector3 CurrentPosition => transform.position;

    public override void Awake()
    {
        base.Awake();

        _unitHealth = GetComponent<UnitHealth>();

        TargetManager.Instance.RegisterToManager(_unitHealth);

        _unitHealth.OnDeath += () =>
        {
            TargetManager.Instance.UnregisterToManager(_unitHealth);
        };

        _unitHealth.OnReset += () =>
        {
            TargetManager.Instance.RegisterToManager(_unitHealth);
        };
    }
}
