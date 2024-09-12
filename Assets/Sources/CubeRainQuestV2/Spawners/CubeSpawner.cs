using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeRainV2
{
	public class CubeSpawner : Spawner<RainyCube>
	{
		[SerializeField] private BombSpawner _bombSpawner;
		
		protected override void OnSpawnedDestroy(RainyCube spawnableObject)
		{
			base.OnSpawnedDestroy(spawnableObject);
			_bombSpawner.SpawnAt(spawnableObject.transform.position);
		}
	}
}
