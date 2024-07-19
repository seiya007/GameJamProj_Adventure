using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockSubject
{
    private List<Action> actionList = new List<Action>();

    public void AddListener(Action act){actionList.Add(act);}
    public bool RemoveListener(Action act){ return actionList.Remove(act);}

    public void Invoke()
    {
        foreach( var act in actionList){ act?.Invoke(); }
    }
}
