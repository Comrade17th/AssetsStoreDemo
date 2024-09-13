using CubeRainV2;
using UnityEngine;
using TMPro;

public class SpawnerView<T> : MonoBehaviour where T: MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _counterTMPro;
    
    [SerializeField] private string _activeLabel = $"Активно: ";
    [SerializeField] private string _entititesLabel = $"Создано: ";
    [SerializeField] private string _spawnsLabel = $"Заспавнено: ";
    
    private void OnEnable()
    {
        _spawner.CounterChanged += OnCounterChanged;
    }

    private void OnDisable()
    {
        _spawner.CounterChanged += OnCounterChanged;
    }

    private void OnCounterChanged(int entitiesCount, int activeCount, int spawnsCount)
    {
        _counterTMPro.text = $"{_entititesLabel} {entitiesCount}\n" +
                             $"{_activeLabel} {activeCount}\n" +
                             $"{_spawnsLabel} {spawnsCount}";
    }
}
