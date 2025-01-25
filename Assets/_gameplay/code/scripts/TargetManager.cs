using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    public static TargetManager Instance => _instance;

    [SerializeField] private List<UnitHealth> _mainTargets = new();

    public List<UnitHealth> MainTargets => _mainTargets;

    public void RegisterToManager(UnitHealth target)
    {
        Debug.Log("Ravi");

        _mainTargets.Add(target);
    }
    public void UnregisterToManager(UnitHealth target)
    {
        _mainTargets.Remove(target);
    }
}
