using DG.Tweening;
using UnityEngine;

public class TemporaryWall : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 15f;
    [SerializeField] private float _scaleMultiplier = 3f;

    private void Start()
    {
        var defaultScale = transform.localScale;

        transform.DOPunchScale(Vector3.one, 0.4f).OnComplete(() =>
        {
            transform.DOScale(defaultScale * _scaleMultiplier, _lifeTime).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
