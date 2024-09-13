using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace CubeRainV2
{
	public class Spawner<T> : MonoBehaviour where T: MonoBehaviour, ISpawnable<T>
	{
		[SerializeField] private T _prefab;
		[SerializeField] private float _delay = 3f;
		[SerializeField] private float _randomSpreadX = 1.1f;
		[SerializeField] private float _randomSpreadZ = 1.1f;
		[SerializeField] private int _startAmount = 1;
		[SerializeField] private bool _isAutoSpawn = false;

		private Pool<T> _pool;
		private int _activeCount = 0;
		private int _spawnsCount = 0;
		private WaitForSeconds _waitSpawn;
		
		public event Action<int, int, int> CounterChanged;
		
		private void Awake()
		{
			_pool = new Pool<T>(_prefab, transform, transform, _startAmount);
			_waitSpawn = new WaitForSeconds(_delay);
		}

		private void Start()
		{
			CounterChanged?.Invoke(_pool.EntitiesCount, _activeCount, _spawnsCount);
			
			if (_isAutoSpawn)
				StartCoroutine(RandomSpawning());
		}
		
		protected virtual void SpawnAtRandom()
		{
			T spawnedObject = Spawn();
			spawnedObject.transform.position = GetRandomSpawnPosition();
		}

		protected Vector3 GetRandomSpawnPosition()
		{
			float spawnPositionX = Random.Range(transform.position.x - _randomSpreadX,
				transform.position.x + _randomSpreadX);
			float spawnPositionZ = Random.Range(transform.position.z - _randomSpreadZ,
				transform.position.z + _randomSpreadZ);
			Vector3 spawnPosition = new Vector3(spawnPositionX,
				transform.position.y,
				spawnPositionZ);

			return spawnPosition;
		}

		protected T Spawn()
		{
			T spawnedObject = _pool.Get();

			spawnedObject.Destroying += OnSpawnedDestroy;
			spawnedObject.gameObject.SetActive(true);
			CounterChanged?.Invoke(_pool.EntitiesCount, ++_activeCount, ++_spawnsCount);
			
			return spawnedObject;
		}

		protected virtual void OnSpawnedDestroy(T spawnableObject)
		{
			spawnableObject.Destroying -= OnSpawnedDestroy;
			spawnableObject.gameObject.SetActive(false);
			_pool.Release(spawnableObject);
			CounterChanged?.Invoke(_pool.EntitiesCount, --_activeCount, _spawnsCount);
		}
		
		private IEnumerator RandomSpawning()
		{
			while (enabled)
			{
				SpawnAtRandom();
				yield return _waitSpawn;
			}
		}
	}
}
