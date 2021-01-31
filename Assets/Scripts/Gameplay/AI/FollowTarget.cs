using System;
using System.Collections;
using Game.Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.AI
{
    public class FollowTarget : MonoBehaviour
    {
        private GameObject _target;

        public UnityEvent onStartFollow = new UnityEvent();
        public UnityEvent onStopFollow = new UnityEvent();
        private bool _shouldFollow = false;

        public float minimumDistanceAway = 1f;
        public float maximumDistanceAway = 3f;
        public AIController controller;

        public Animator _Animator;
        
        private IMove _moveInterface;
        private IMove _moveTargetInterface;

        private float _defaultSpeed;

        private Coroutine _followRoutine = null;

        private void Start()
        {
            controller ??= GetComponent<AIController>();
            _Animator ??= GetComponent<Animator>();
            _moveInterface = GetComponent<IMove>();
            _defaultSpeed = _moveInterface.GetSpeed();
        }

        private IEnumerator FollowLogic()
        {
            while (_shouldFollow)
            {
                Vector2 targetPosition = _target.transform.position;
                Vector2 ourPosition = transform.position;
                
                Vector2 moveDirection = targetPosition - ourPosition;
                moveDirection.Normalize();
                
                float distanceFromTarget = Vector2.Distance(ourPosition, targetPosition);
                
                if (distanceFromTarget < minimumDistanceAway)
                {
                    controller.SetMoveDirection(Vector2.zero);
                }
                else
                {
                    if( distanceFromTarget > maximumDistanceAway)
                    {
                        ResetMove();
                    } else if (_moveTargetInterface.GetSpeed() < _moveInterface.GetSpeed())
                    {
                        _moveInterface.SetSpeed(_moveTargetInterface.GetSpeed() * 0.9f);
                    }
                    controller.SetMoveDirection(moveDirection);
                }
                yield return null;
            }

            ResetMove();
            _followRoutine = null;
            yield break;
        }
        public void Follow(GameObject target)
        {
            _target = target;
            _moveTargetInterface = target.GetComponent<IMove>();
            
            _shouldFollow = true;
            
            if(_Animator)
                _Animator.SetBool("Follow",true);
            
            onStartFollow?.Invoke();
            if (_followRoutine == null)
                _followRoutine = StartCoroutine(FollowLogic());
        }

        public void StopFollow()
        {
            if(_Animator)
                _Animator.SetBool("Follow",false);
            
            _shouldFollow = false;
            ResetMove();
            onStopFollow?.Invoke();
        }

        private void ResetMove()
        {
            _moveInterface.SetSpeed(_defaultSpeed);
            controller.SetMoveDirection(Vector2.zero);
        }
    }
}
