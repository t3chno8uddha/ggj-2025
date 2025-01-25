using UnityEngine;

public class MainBase : MonoBehaviour
{
    private UnitHealth _unitHealth;

    private void Awake()
    {
        _unitHealth = GetComponent<UnitHealth>();

        TargetManager.Instance.RegisterToManager(_unitHealth);

        _unitHealth.OnDeath += () =>
        {
            TargetManager.Instance.UnregisterToManager(_unitHealth);
        };
    }
}
