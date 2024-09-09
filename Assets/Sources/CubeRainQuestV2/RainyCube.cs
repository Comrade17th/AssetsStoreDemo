using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeRainV2
{
	[RequireComponent(typeof(Rigidbody),
		typeof(MeshRenderer))]
	public class RainyCube : MonoBehaviour, ISpawnable
	{
		public event Action<ISpawnable, Vector3> NeedDestroy; 
		
		[SerializeField] private Color _defaultColor;
		[SerializeField] private float _minLifetime = 2f;
		[SerializeField] private float _maxLifetime = 5f;

		private bool _isCollided = false;
		
		private MeshRenderer _meshRenderer;
		private Rigidbody _rigidbody;
		private Coroutine _coroutine;
		private WaitForSeconds _waitLifetime;

		private void Awake()
		{
			_meshRenderer = GetComponent<MeshRenderer>();
			_rigidbody = GetComponent<Rigidbody>();
		}
		
		private void OnEnable()
		{
			Reset();
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent(out Obstucle obstucle))
			{
				OnObstucleCollision();
			}
		}

		public void Destroy()
		{
			gameObject.SetActive(false);
		}

		private void OnObstucleCollision()
		{
			if (_isCollided == false)
			{
				_meshRenderer.material.color = Random.ColorHSV();
				_isCollided = true;
				
				if (_coroutine != null)
					StopCoroutine(_coroutine);
			
				_coroutine = StartCoroutine(Countdown());
			}
		}

		private IEnumerator Countdown()
		{
			_waitLifetime = new WaitForSeconds(Random.Range(_minLifetime, _maxLifetime));
			yield return _waitLifetime;
			NeedDestroy?.Invoke(this, transform.position);
		}

		private void Reset()
		{
			_meshRenderer.material.color = _defaultColor;
			_rigidbody.velocity = Vector3.zero;
			_isCollided = false;
		}
	}
}

