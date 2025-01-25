using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    [SerializeField] private List<UnitHealth> _mainTargets = new();

    public List<UnitHealth> MainTargets => _mainTargets;


    public void RegisterToManager(UnitHealth target)
    {
        Instance._mainTargets.Add(target);
    }
    public void UnregisterToManager(UnitHealth target)
    {
        Instance._mainTargets.Remove(target);
    }
}
