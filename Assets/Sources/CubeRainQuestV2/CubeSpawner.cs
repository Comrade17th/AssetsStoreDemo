using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeRainV2
{
	public class CubeSpawner : Spawner<RainyCube>
	{
		[SerializeField] private BombSpawner _bombSpawner;

		protected override void SpawnAtRandom()
		{
			RainyCube spawnedObject = _pool.Peek();
			spawnedObject.SetBombSpawner(_bombSpawner);
			spawnedObject.transform.position = GetRandomSpawnPosition();
		}
	}
}
