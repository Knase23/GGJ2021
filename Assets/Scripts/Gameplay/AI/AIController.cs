using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.Interfaces;
using UnityEngine;

public class AIController : MonoBehaviour, IMoveController
{
    // Start is called before the first frame update
    private Vector2 _moveDirection;
    private Vector2 _directionToMove;

    public enum WalkDirection
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }
    public void SetMoveDirection(int direction)
    {
        Vector2 dir = Vector2.zero;
        switch ((WalkDirection)direction)
        {
            case WalkDirection.Right:
                dir = Vector2.right;
                break;
            case WalkDirection.Left:
                dir = Vector2.left;
                break;
        }
        SetMoveDirection(dir);
    }
    public void SetMoveDirection(Vector2 direction)
    {
        _directionToMove = direction; // Save it, so we know if we want to stop. Anytime, we know which direction it wants to go in
        _moveDirection = direction; // The actual direction it will go in.
    }

    public Vector2 GetDirection()
    {
        return _moveDirection;
    }
}
