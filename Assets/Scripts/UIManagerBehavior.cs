using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _winPrompt;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private GameObject _player;

    private PlayerTimerBehavior _timer;

    private bool _validTimerAndTextFound;

    public void Start()
    {
        _validTimerAndTextFound = false;

        // check if the given player is valid, has a PlayerTimerBehavior, and if the given text buffer is valid 
        if (_player && _player.TryGetComponent(out PlayerTimerBehavior timer) && _timerText)
        {
            _timer = timer;
            _validTimerAndTextFound = true;
        }
    }

    public void LateUpdate()
    {
        if (!_validTimerAndTextFound)
            return;

        _timerText.text = _timer.Timer.ToString("Time: 0.0");
    }
    public void ActivateWinPopup()
    {
        _winPrompt.SetActive(true);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
