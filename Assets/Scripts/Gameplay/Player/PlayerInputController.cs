using System;
using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerInputController : MonoBehaviour, IMoveController
    {

        public InputActionReference movement;

        private void OnEnable()
        {
            movement.action.Enable();
        }

        private void OnDisable()
        {
            movement.action.Disable();
        }


        public Vector2 GetDirection()
        {
            return movement.action.ReadValue<Vector2>();
        }
    }
}
