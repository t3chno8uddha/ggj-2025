using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplayer : MonoBehaviour
{
    [SerializeField] protected UnitHealth _unitHealth;
    [SerializeField] protected Image _fill;

    protected Tweener _fillTween;
    // Update is called once per frame

    protected void Start()
    {
        _unitHealth.OnDamageReceived += UpdateHealth;

        _unitHealth.OnDeath += HidePanel;

        _unitHealth.OnReset += ShowPanel;
        _unitHealth.OnReset += UpdateHealth;

        UpdateHealth();
    }

    protected virtual void UpdateHealth()
    {
        if (_fillTween != null && _fillTween.IsPlaying())
        {
            _fillTween.Kill();
        }

        var endValue = (float)_unitHealth.CurrentHealth / _unitHealth.MaxHealth;
        _fillTween = _fill.DOFillAmount(endValue, 0.1f);
    }

    protected void HidePanel()
    {
        gameObject.SetActive(false);
    }

    protected void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
