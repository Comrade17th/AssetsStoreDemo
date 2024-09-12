using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public abstract class SpawnableObject : MonoBehaviour
{
    public event Action<SpawnableObject> NeedDestoy;
    
    protected virtual void OnNeedDestroy()
    {
            NeedDestoy?.Invoke(this);
    }
}
