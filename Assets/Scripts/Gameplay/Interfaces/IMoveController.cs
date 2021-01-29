using System;
using UnityEngine;

namespace Game.Gameplay.Interfaces
{
    public interface IMoveController
    {
        Vector2 GetDirection();
    }

    public interface IMove
    {
        float GetSpeed();
        void SetSpeed(float value);
    }
}
