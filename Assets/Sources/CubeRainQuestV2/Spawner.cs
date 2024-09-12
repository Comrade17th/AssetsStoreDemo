using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace CubeRainV2
{
	public class Spawner<T> : MonoBehaviour where T: SpawnableObject
	{
		public event Action<int, int, int> CounterChanged;
		
		[SerializeField] private T _prefab;
		[SerializeField] private float _delay = 3f;
		[SerializeField] private float _randomSpreadX = 1.1f;
		[SerializeField] private float _randomSpreadZ = 1.1f;
		[SerializeField] private int _startAmount = 1;
		[SerializeField] private bool _isAutoSpawn = false;

		protected Pool<T> _pool;
		private WaitForSeconds _waitSpawn;

		private int _entitiesCount => _pool.EntitiesCount;
		private int _activeCount = 0;
		private int _spawnsCount = 0;

		private void Awake()
		{
			_pool = new Pool<T>(_prefab, transform, transform, _startAmount);
			_waitSpawn = new WaitForSeconds(_delay);
		}

		private void Start()
		{
			CounterChanged.Invoke(_entitiesCount, _activeCount, _spawnsCount);
			
			if (_isAutoSpawn)
				StartCoroutine(RandomSpawning());
		}

		public void SpawnAt(Vector3 position)
		{
			T spawnedObject = Spawn();
			spawnedObject.transform.position = position;
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

		private T Spawn()
		{
			T spawnedObject = _pool.Get();

			spawnedObject.NeedDestroy += OnSpawnedDestroy;
			
			spawnedObject.gameObject.SetActive(true);
			CounterChanged.Invoke(_entitiesCount, ++_activeCount, ++_spawnsCount);
			return spawnedObject;
		}

		private void OnSpawnedDestroy(T spawnableObject)
		{
			spawnableObject.gameObject.SetActive(false);
			_pool.Return(spawnableObject);
			CounterChanged.Invoke(_entitiesCount, --_activeCount, _spawnsCount);
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
