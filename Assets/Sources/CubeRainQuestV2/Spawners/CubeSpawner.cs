using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
namespace CubeRainV2
{
	public class CubeSpawner : Spawner<RainyCube>
	{
		[SerializeField] private BombSpawner _bombSpawner;

		private void OnEnable()
		{
			_pool.SpawnableDestroyed += _bombSpawner.SpawnAt;
		}

		protected override void SpawnAtRandom()
		{
			RainyCube spawnedObject = _pool.Peek();
			spawnedObject.transform.position = GetRandomSpawnPosition();
		}
	}
}
*/