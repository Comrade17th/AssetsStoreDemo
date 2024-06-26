using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QuestTimer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _timeStep = 0.5f;
        [SerializeField] private float _valueStep = 1f;

        private bool _isWork = false;
        private float _value = 0f;
        private WaitForSeconds _waitStep;
        private Coroutine _coroutine;

        public event Action<float> Changed;

        private void Awake()
        {
            _waitStep = new WaitForSeconds(_timeStep);
        }

        private IEnumerator Count()
        {
            while (_isWork)
            {
                yield return _waitStep;
                _value += _valueStep;
                Changed?.Invoke(_value);
            }
        }

        public void Launch()
        {
            Stop();

            _isWork = true;
            StartCoroutine(Count());
        }

        public void Stop()
        {
            _isWork = false;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }
}
