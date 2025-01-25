using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplayer : MonoBehaviour
{
    [SerializeField] private UnitHealth _unitHealth;
    [SerializeField] private Image _fill;

    private Tweener _fillTween;
    // Update is called once per frame

    private void Start()
    {
        _unitHealth.OnDamageReceived += UpdateHealth;

        _unitHealth.OnDeath += HidePanel;

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (_fillTween != null && _fillTween.IsPlaying())
        {
            _fillTween.Kill();
        }

        var endValue = (float)_unitHealth.CurrentHealth / _unitHealth.MaxHealth;
        _fillTween = _fill.DOFillAmount(endValue, 0.1f);
    }

    private void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
