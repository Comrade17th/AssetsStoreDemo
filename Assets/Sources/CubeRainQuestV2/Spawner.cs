using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeRainV2
{
	public class Spawner : MonoBehaviour
	{
	[SerializeField] private MonoBehaviour _prefab;
	[SerializeField] private float _delay = 3f;
	[SerializeField] private float _randomSpreadX = 1.1f;
	[SerializeField] private float _randomSpreadZ = 1.1f;
	[SerializeField] private int _startAmount = 1;
	[SerializeField] private bool _isAutoSpawn = false;

	private Pool<MonoBehaviour> _pool;
	private WaitForSeconds _waitSpawn;

	private void Awake()
	{
		_pool = new Pool<MonoBehaviour>(_prefab, transform, transform, _startAmount);
		_waitSpawn = new WaitForSeconds(_delay);
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
		MonoBehaviour spawnedObject = _pool.Peek();
		spawnedObject.transform.position = position;
	}

	private IEnumerator RandomSpawning()
	{
		while (enabled)
		{
			SpawnAtRandom();
			yield return _waitSpawn;
		}
	}

	private void SpawnAtRandom()
	{
		float spawnPositionX = Random.Range(transform.position.x - _randomSpreadX,
			transform.position.x + _randomSpreadX);
		float spawnPositionZ = Random.Range(transform.position.z - _randomSpreadZ,
			transform.position.z + _randomSpreadZ);
		Vector3 spawnPosition = new Vector3(spawnPositionX,
			transform.position.y,
			spawnPositionZ);

		MonoBehaviour spawnedObject = _pool.Peek();
		spawnedObject.transform.position = spawnPosition;
	}
	}
}
