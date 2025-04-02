using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTimerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _timerDuration = 15.0f;

    private float _timer;

    public UnityEvent OnTimerEnd;

    public float Timer { get => _timer; }

    private void Awake()
    {
        // Automatically add PlayerResetBehavior's Restart function if there is one on the player
        if (gameObject.TryGetComponent(out PlayerResetBehavior reset))
        {
            OnTimerEnd.AddListener(reset.Restart);
        }
    }
    private void Start()
    {
        _timer = _timerDuration;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0.0f)
        {
            OnTimerEnd.Invoke();
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        _timer = _timerDuration;
    }
}
