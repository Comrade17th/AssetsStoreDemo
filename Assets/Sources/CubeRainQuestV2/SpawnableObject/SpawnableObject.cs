using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public abstract class SpawnableObject : MonoBehaviour
{
    public event Action<SpawnableObject> Destroying;
    
    protected virtual void OnDestroying()
    {
	    Destroying?.Invoke(this);
    }
}
