using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace CubeRainV2
{
	public class Spawner<T> : MonoBehaviour where T: MonoBehaviour, ISpawnable
	{
		public event Action<int> EntitiesCountChanged;
		public event Action<int> SpawnsCountChanged;
		public event Action<int> ActiveCountChanged;
		
		[SerializeField] private T _prefab;
		[SerializeField] private float _delay = 3f;
		[SerializeField] private float _randomSpreadX = 1.1f;
		[SerializeField] private float _randomSpreadZ = 1.1f;
		[SerializeField] private int _startAmount = 1;
		[SerializeField] private bool _isAutoSpawn = false;

		protected Pool<T> _pool;
		private WaitForSeconds _waitSpawn;

		private void Awake()
		{
			_pool = new Pool<T>(_prefab, transform, transform, _startAmount);
			_waitSpawn = new WaitForSeconds(_delay);
		}

		private void OnEnable()
		{
			_pool.EntitiesCountChanged += OnEntitiesCountChanged;
			_pool.ActiveCountChanged += OnActiveCountChanged;
			_pool.SpawnsCountChanged += OnSpawnsCountChanged;
		}

		private void OnDisable()
		{
			_pool.EntitiesCountChanged -= OnEntitiesCountChanged;
			_pool.ActiveCountChanged -= OnActiveCountChanged;
			_pool.SpawnsCountChanged -= OnSpawnsCountChanged;
		}

		private void Start()
		{
			if (_isAutoSpawn)
				StartCoroutine(RandomSpawning());
		}

		public void Reset()
		{
			_pool.Reset();
		}

		public void SpawnAt(Vector3 position)
		{
			T spawnedObject = _pool.Peek();
			spawnedObject.transform.position = position;
		}

		private void OnEntitiesCountChanged(int count)
		{
			EntitiesCountChanged?.Invoke(count);
			Debug.Log($"Entites {count}");
		}
		
		private void OnSpawnsCountChanged(int count)
		{
			SpawnsCountChanged?.Invoke(count);
			Debug.Log($"Spawns {count}");
		}
		
		private void OnActiveCountChanged(int count)
		{
			ActiveCountChanged?.Invoke(count);
			Debug.Log($"Active {count}");
		}

		private IEnumerator RandomSpawning()
		{
			while (enabled)
			{
				SpawnAtRandom();
				yield return _waitSpawn;
			}
		}

		protected virtual void SpawnAtRandom()
		{
			T spawnedObject = _pool.Peek();
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
	}
}
