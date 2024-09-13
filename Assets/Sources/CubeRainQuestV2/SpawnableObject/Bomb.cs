using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace CubeRainV2
{
	[RequireComponent(typeof(Explosion))]
	public class Bomb : MonoBehaviour, ISpawnable<Bomb>
	{
		[SerializeField] private float _minTimeExlposion = 2;
		[SerializeField] private float _maxTimeExlposion = 5;

		private float _alphaTransparent = 0f;
		private float _alphaOpaque = 1f;
		
		private MeshRenderer _renderer;
		private Coroutine _coroutine;
		private WaitForSeconds _waitExplode;
		private Explosion _explosion;
		private Color _defaultColor;
		
		public event Action<Bomb> Destroying; 
		
		private void Awake()
		{
			_explosion = GetComponent<Explosion>();
			_renderer = GetComponent<MeshRenderer>();
			_defaultColor = _renderer.material.color;
		}

		private void OnEnable()
		{
			_renderer.material.color = _defaultColor;
			
			if (_coroutine != null)
				StopCoroutine(_coroutine);
			
			_coroutine = StartCoroutine(GettingExplode());
		}
		
		private IEnumerator GettingExplode()
		{
			float time = Random.Range(_minTimeExlposion, _maxTimeExlposion);
			_waitExplode = new WaitForSeconds(time);

			_renderer.material.DOFade(_alphaTransparent, time);
			yield return _waitExplode;
			
			_explosion.Explode();
			Destroying?.Invoke(this);
		}
	}    
}
