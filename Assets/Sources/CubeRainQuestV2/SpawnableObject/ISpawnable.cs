using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable<T>
{
    public event Action<T> Destroying;
}
