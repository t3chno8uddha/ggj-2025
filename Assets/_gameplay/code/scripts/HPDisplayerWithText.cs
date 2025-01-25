using DG.Tweening;
using TMPro;
using UnityEngine;

public class HPDisplayerWithText : HPDisplayer
{
    [SerializeField] protected TMP_Text _text;
    [SerializeField] protected int _warningAt = 5;

    private Tweener _textTween;

    private void OnEnable()
    {
        _text.color = Color.black;
    }

    protected override void UpdateHealth()
    {
        base.UpdateHealth();

        _text.SetText($"{_unitHealth.CurrentHealth}");

        _text.color = Color.black;

        if (_unitHealth.CurrentHealth > _warningAt)
        {
            if (_textTween != null)
            {
                _textTween.Kill();
            }

            _text.color = Color.black;
        }
        else
        {
            _textTween.Kill();
            _text.color = Color.black;

            _textTween = _text.DOColor(Color.red, 0.4f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
