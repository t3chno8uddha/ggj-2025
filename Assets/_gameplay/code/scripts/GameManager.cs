using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnitHealth _mainBase;
    [SerializeField] private CanvasGroup _gameOverPanel;
    [SerializeField] private UnitHealth _player;
    [SerializeField] private CanvasGroup _respawnPanel;
    // Start is called before the first frame update
    void Start()
    {
        _gameOverPanel.gameObject.SetActive(false);
        //_respawnPanel.gameObject.SetActive(false);

        _mainBase.OnDeath += () =>
        {
            Debug.Log("Game Over");
            _gameOverPanel.gameObject.SetActive(true);
            _gameOverPanel.alpha = 0;
            _gameOverPanel.DOFade(1, 1.5f);
        };
    }

    private IEnumerable GameOver()
    {
        yield return null;
    }
}
