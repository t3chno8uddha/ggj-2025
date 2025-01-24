public class Player : Singleton<Player>
{
    // Start is called before the first frame update
    public static Player Instance => _instance;
    public override void Awake()
    {
        base.Awake();
    }
}
