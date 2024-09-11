using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace CubeRainV2
{
	public class Pool<Template> where Template : MonoBehaviour, ISpawnable
	{
		public event Action<int> EntitiesCountChanged;
		public event Action<int> SpawnsCountChanged;
		public event Action<int> ActiveCountChanged;

		public event Action<Vector3> SpawnableDestroyed;
		
		private readonly List<Template> _pool = new();

		private readonly Template _prefab;
		private readonly Transform _container;
		private readonly Transform _spawnPoint;
		private readonly int _startAmount;

		private int _entitiesCount = 0;
		private int _spawnsCount = 0;
		private int _activeCount = 0;

		private int PoolCount => _pool.Count;

		public Pool(Template prefab, Transform container, Transform spawnPoint, int startAmount)
		{
			_prefab = prefab;
			_container = container;
			_spawnPoint = spawnPoint;
			_startAmount = startAmount;

			for (int i = 0; i < _startAmount; i++)
			{
				Create();
			}
		}

		public void Reset()
		{
			foreach (Template template in _pool)
			{
				template.gameObject.SetActive(false);
			}

			_activeCount = 0;
			ActiveCountChanged?.Invoke(_activeCount);
		}

		public Template Peek()
		{
			if (TryGetObject(out Template template) == false)
			{
				template = Create();
			}

			template.gameObject.SetActive(true);
			_activeCount++;
			ActiveCountChanged?.Invoke(_activeCount);
			return template;
		}

		private bool TryGetObject(out Template template)
		{
			template = null;

			for (int i = 0; i < PoolCount; i++)
			{
				if (_pool[i].gameObject.activeInHierarchy == false)
				{
					template = _pool[i];
					return true;
				}
			}

			return false;
		}

		private Template Create()
		{
			
			Template template = UnityEngine.Object.Instantiate(_prefab, _container, _spawnPoint);
			template.NeedDestroy += OnTemplateDestroy;
			_entitiesCount++;
			EntitiesCountChanged?.Invoke(_entitiesCount);
			Debug.Log($"Created {_entitiesCount}");
			template.gameObject.SetActive(false);
			_pool.Add(template);

			return template;
		}

		private void OnTemplateDestroy(ISpawnable spawnable, Vector3 position)
		{
			_activeCount--;
			ActiveCountChanged?.Invoke(_activeCount);
			spawnable.Destroy();
			SpawnableDestroyed?.Invoke(position);
		}
	}
}