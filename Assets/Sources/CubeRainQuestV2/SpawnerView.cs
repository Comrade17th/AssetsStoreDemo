using System;
using System.Collections;
using System.Collections.Generic;
using CubeRainV2;
using UnityEngine;
using TMPro;

public class SpawnerView<T> : MonoBehaviour where T: Spawner<MonoBehaviour, ISpawnable>
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _activeCount;
    [SerializeField] private TextMeshProUGUI _entitiesCount;
    [SerializeField] private TextMeshProUGUI _spawnsCount;
    [SerializeField] private string _activeLabel = $"Активно: ";
    [SerializeField] private string _entititesLabel = $"Создано";
    [SerializeField] private string _spawnsLabel = $"Заспавнено";
    
    private void OnEnable()
    {
        _spawner.ActiveCountChanged += OnActiveCountChanged;
        _spawner.EntitiesCountChanged += OnEntitiesCountChanged;
        _spawner.SpawnsCountChanged += OnSpawnsCountChanged;
    }

    private void OnDisable()
    {
        _spawner.ActiveCountChanged -= OnActiveCountChanged;
        _spawner.EntitiesCountChanged -= OnEntitiesCountChanged;
        _spawner.SpawnsCountChanged -= OnSpawnsCountChanged;
    }

    private void OnActiveCountChanged(int count)
    {
        _activeCount.text = $"{_activeLabel} {count}";
    }
    
    private void OnEntitiesCountChanged(int count)
    {
        _entitiesCount.text = $"{_entititesLabel} + {count}";
    }
    
    private void OnSpawnsCountChanged(int count)
    {
        _spawnsCount.text = $"{_spawnsLabel} + {count}";
    }
    
}
