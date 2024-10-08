using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState<T>
{
    protected T _owner;

    public BaseState(T owner)
    { 
        _owner = owner; 
    }

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
