using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private Vector2 _movement = Vector2.zero;
    

    public Vector2 Movement { get { return _movement; } }

    public void MovementSet(Vector2 movement)
    {
        _movement.x = movement.x;
        _movement.y = movement.y;
    }
}
