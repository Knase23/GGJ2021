using System;
using System.Collections;
using System.Collections.Generic;
using Game.Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game
{
    public class Movement : MonoBehaviour, IMove
    {
        private Rigidbody2D _rigidbody2D;
        public SpriteRenderer spriteRenderer;

        public float speed = 10;
        public MonoBehaviour moveController;

        public MovementTutorial tutorial;
        
        public bool allowLeftRightMovement = true;
        public bool allowUpDownMovement = false;
        
        IMoveController Controller
        {
            get { return moveController as IMoveController; }
        }

        private Animator _animator;
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if(spriteRenderer)
                _animator = GetComponent<Animator>();
            if(moveController is PlayerInputController)
                _animator.SetTrigger("GameHasStarted");
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 inputs = Controller.GetDirection();
            Vector2 currentVelocity = _rigidbody2D.velocity;
            currentVelocity = new Vector2(0, currentVelocity.y);

            if (_rigidbody2D.isKinematic)
                currentVelocity = Vector2.zero;
            
            if (allowLeftRightMovement)
            {
                if (inputs.x > 0)
                {
                    currentVelocity.x = speed;
                    if (spriteRenderer) spriteRenderer.flipX = false;
                }

                //transform.position += (Vector3)GroundOffset(Vector3.right) * (speed * Time.deltaTime);
                if (inputs.x < 0)
                {
                    currentVelocity.x = -speed;
                    if (spriteRenderer) spriteRenderer.flipX = true;
                }
            }

            if (allowUpDownMovement)
            {
                if (inputs.y > 0)
                {
                    currentVelocity.y = speed;
                }
                
                if(inputs.y < 0)
                {
                    currentVelocity.y = -speed;
                }
                    
            }
            _rigidbody2D.velocity = currentVelocity;
            // _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, speed);
            // _rigidbody2D.velocity =
            //     new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -speed, speed), _rigidbody2D.velocity.y);
            // transform.position += (Vector3)GroundOffset(Vector3.left) * (speed * Time.deltaTime);

            if (_animator)
            {
                if (_rigidbody2D.velocity.magnitude <= 0.5f)
                {
                    if (tutorial != null)
                    {
                        if (tutorial.isVisible)
                        {
                            tutorial.RemoveMe();
                        }
                    }
                    _animator.SetBool("Walking", false);
                }
                else
                    _animator.SetBool("Walking", true);
            }
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

        public float GetSpeed()
        {
            return speed;
        }

        public void SetSpeed(float value)
        {
            this.speed = value;
        }
    }
}