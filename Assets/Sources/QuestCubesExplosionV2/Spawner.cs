using UnityEngine;

namespace QuestExplosiveCubeV2
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField] private ExplosiveCube _cubePrefab;
		
		private int _minSpawnCount = 2;
		private int _maxSpawnCount = 6;
		
		public ExplosiveCube[] SpawnGroup(float currentSplitChance)
		{
			int count = Random.Range(_minSpawnCount, _maxSpawnCount);
			ExplosiveCube[] cubes = new ExplosiveCube[count];

			for (int i = 0; i < count; i++)
				cubes[i] = SpawnCube(currentSplitChance);

			return cubes;
		}
		
		private ExplosiveCube SpawnCube(float currentSplitChance)
		{
			ExplosiveCube cube = Instantiate(_cubePrefab, transform.position, transform.rotation);
			cube.Initialize(transform.localScale, currentSplitChance);
			return cube;
		}
	}    
}

