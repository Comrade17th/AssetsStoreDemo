using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _timer.Changed += UpdateTime;
    }

    private void OnDisable()
    {
        _timer.Changed -= UpdateTime;
    }

    private void UpdateTime(float time)
    {
        _text.text = $"{(int)time}";
    }
}
