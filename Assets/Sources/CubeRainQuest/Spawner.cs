using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeRain
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField] private RainyCube _prefab;
		[SerializeField] private float _delay = 3f;
		[SerializeField] private float _randomSpreadX = 1.1f;
		[SerializeField] private float _randomSpreadZ = 1.1f;
		[SerializeField] private int _startAmount = 1;
    
		private Pool<RainyCube> _pool;
		private WaitForSeconds _waitSpawn;

		private void Awake()
		{
			_pool = new Pool<RainyCube>(_prefab, transform, transform, _startAmount);
			_waitSpawn = new WaitForSeconds(_delay);
		}

		private void Start()
		{
			StartCoroutine(Spawning());
		}

		public void Reset()
		{
			_pool.Reset();
		}

		private IEnumerator Spawning()
		{
			while (enabled)
			{
				Spawn();
				yield return _waitSpawn;
			}
		}

		private void Spawn()
		{
			float spawnPositionX = Random.Range(
				transform.position.x - _randomSpreadX,
				transform.position.x + _randomSpreadX);
			float spawnPositionZ = Random.Range(
				transform.position.z - _randomSpreadZ,
				transform.position.z + _randomSpreadZ);
			Vector3 spawnPosition = new Vector3(
				spawnPositionX,
				transform.position.y,
				spawnPositionZ);
      
			RainyCube cube = _pool.Peek();
			cube.transform.position = spawnPosition;
		}
	}
}
