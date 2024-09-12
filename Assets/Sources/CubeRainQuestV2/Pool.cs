using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace CubeRainV2
{
	public class Pool<Template> where Template : SpawnableObject
	{
		public int EntitiesCount => _entitiesCount;
		
		private readonly List<Template> _pool = new();
		private Queue<Template> _queue = new();
		private readonly Template _prefab;
		private readonly Transform _container;
		private readonly Transform _spawnPoint;
		private readonly int _startAmount;

		private int _entitiesCount = 0;

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
		
		public void Return(Template template)
		{
			template.gameObject.SetActive(false);
			_queue.Enqueue(template);
		}
		
		public Template Get()
		{
			if (_queue.TryDequeue(out Template template) == false)
			{
				Create();
				template = _queue.Dequeue();
			}
			
			return template;
		}
		
		private Template Create()
		{
			Template template = UnityEngine.Object.Instantiate(_prefab, _container, _spawnPoint);
			_entitiesCount++;
			template.gameObject.SetActive(false);
			_pool.Add(template);
			_queue.Enqueue(template);

			return template;
		}
	}
}