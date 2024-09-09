using System.Collections;
using System.Collections.Generic;
using CubeRainV2;
using UnityEngine;

public class SpawnerView : MonoBehaviour where T: MonoBehaviour, ISpawnable
{
    [SerializeField] private Spawner<T> _spawner;
}
