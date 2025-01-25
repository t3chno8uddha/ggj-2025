using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnitHealth _mainBase;
    [SerializeField] private CanvasGroup _gameOverPanel;
    [SerializeField] private GameObject[] _objectsToDisableOnGameOver;
    [SerializeField] private int _resetLevelIn = 4;
    [SerializeField] private UnitHealth _player;
    [SerializeField] private CanvasGroup _respawnPanel;
    [SerializeField] private GameObject[] _objectsToDisableOnPlayerDeath;
    [SerializeField] private TMP_Text _respawnText;
    [SerializeField] private int _reviveTime = 5;
    [SerializeField] private Transform _respawnPoint;

    private PlayerMovement _playerMovement;

    void Start()
    {
        _gameOverPanel.gameObject.SetActive(false);
        _respawnPanel.gameObject.SetActive(false);

        _mainBase.OnDeath += StartResetLevelCoroutine;

        _player.OnDeath += RespawnPlayerCoroutine;

        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    private void StartResetLevelCoroutine()
    {
        StartCoroutine(RestartLevel());
    }

    private void RespawnPlayerCoroutine()
    {
        if (_mainBase.IsDead) return;

        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RestartLevel()
    {
        Debug.Log("Game Over");

        foreach (var item in _objectsToDisableOnGameOver)
        {
            item.SetActive(false);
        }

        _respawnPanel.gameObject.SetActive(false);
        _gameOverPanel.gameObject.SetActive(true);
        _gameOverPanel.alpha = 0;
        _gameOverPanel.DOFade(1, 1.5f);

        yield return new WaitForSeconds(_resetLevelIn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator RespawnPlayer()
    {
        _respawnPanel.gameObject.SetActive(true);
        _respawnPanel.alpha = 0;
        _respawnPanel.DOFade(1, 0.5f);

        foreach (var item in _objectsToDisableOnGameOver)
        {
            item.SetActive(false);
        }

        _playerMovement.enabled = false;

        for (int i = _reviveTime; i >= 0; i--)
        {
            _respawnText.SetText($"Bubbling up in {i}");

            yield return new WaitForSeconds(1f);
        }

        _player.transform.position = _respawnPoint.position;

        _player.ResetHp();

        _respawnPanel.gameObject.SetActive(false);

        foreach (var item in _objectsToDisableOnGameOver)
        {
            item.SetActive(true);
        }

        _playerMovement.enabled = true;
    }
}
