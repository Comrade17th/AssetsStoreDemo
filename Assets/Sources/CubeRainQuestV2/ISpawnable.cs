using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public event Action<ISpawnable, Vector3> NeedDestroy; 
    
    public void Destroy();
}
