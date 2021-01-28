using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class Movement : MonoBehaviour
    {
        public InputActionReference actionBinding;
        private Rigidbody2D _rigidbody2D;
        public float speed = 10;
        private Vector2 inputs => actionBinding.action.ReadValue<Vector2>();

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            actionBinding?.action.Enable();
        }

        private void OnEnable()
        {
            actionBinding?.action.Enable();
        }

        private void OnDisable()
        {
            actionBinding?.action.Disable();
        }

        // Update is called once per frame
        void Update()
        {
            if (inputs.x > 0)
                _rigidbody2D.AddForce(Vector2.right * speed,ForceMode2D.Force);
            //transform.position += (Vector3)GroundOffset(Vector3.right) * (speed * Time.deltaTime);
            if (inputs.x < 0)
                _rigidbody2D.AddForce(Vector2.left * speed,ForceMode2D.Force);
            
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity,speed);
            // transform.position += (Vector3)GroundOffset(Vector3.left) * (speed * Time.deltaTime);
        }

        public Vector2 GroundOffset(Vector2 moveDirection)
        {
            moveDirection.Normalize();
            //Check 
            float distance = 2;
            LayerMask groundMask = LayerMask.GetMask(("ground"));

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, (moveDirection + Vector2.down).normalized,
                distance, groundMask);

            if (raycastHit2D.collider == null)
                return moveDirection;

            return (moveDirection + Vector2.up).normalized;
            ;
        }
    }
}