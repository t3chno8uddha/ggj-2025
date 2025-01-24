using System.Collections.Generic;

public class TargetManager : Singleton<TargetManager>
{
    public static TargetManager Instance => _instance;
    private List<UnitHealth> _mainTargets;

    public List<UnitHealth> MainTargets => _mainTargets;

    public static UnitHealth ReturnClosestTarget()
    {
        return null;
    }

    public void RegisterToManager(UnitHealth target)
    {
        _mainTargets.Add(target);
    }
    public void UnregisterToManager(UnitHealth target)
    {
        _mainTargets.Remove(target);
    }
}
