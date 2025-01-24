public class Player : Singleton<Player>, IMainTarget
{
    // Start is called before the first frame update
    private UnitHealth _unitHealth;
    public static Player Instance => _instance;
    public override void Awake()
    {
        base.Awake();

        _unitHealth = GetComponent<UnitHealth>();

        TargetManager.Instance.RegisterToManager(this);

        _unitHealth.OnDeath += () =>
        {
            TargetManager.Instance.UnregisterToManager(this);
        };
    }
}
