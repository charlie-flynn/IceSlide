using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTimerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _timerDuration;

    private float _timer;

    public UnityEvent OnTimerEnd;

    private void Start()
    {
        _timer = _timerDuration;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0.0f)
            OnTimerEnd.Invoke();
    }
}
