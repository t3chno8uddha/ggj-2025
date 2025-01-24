using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    public static TargetManager Instance => _instance;

    public List<IMainTarget> _mainTargets;

    public void RegisterToManager(IMainTarget target)
    {
        _mainTargets.Add(target);
    }
    public void UnregisterToManager(IMainTarget target)
    {
        _mainTargets.Remove(target);
    }
}
