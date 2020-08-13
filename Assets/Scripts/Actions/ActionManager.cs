using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private IAction _current = null;

    public void StartAction(IAction action)
    {
        if (_current == action)
            return;

        if (_current != null)
            _current.End();

        _current = action;
    }
}
