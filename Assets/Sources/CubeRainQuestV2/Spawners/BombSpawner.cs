using System.Collections;
using System.Collections.Generic;
using CubeRainV2;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void SpawnAt(Vector3 position)
    {
        Bomb bomb = Spawn();
        bomb.transform.position = position;
    }
}
