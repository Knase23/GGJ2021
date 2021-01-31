using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.AI
{
    public class BeFollowedAI : MonoBehaviour
    {
        public AIController controller;
        public Transform target;
        public GameObject objectThatFollow;

        public Renderer primaryRenderer;
        
        private Coroutine _walkRoutine;

        public Transform catchUpPoint;

        public float minimumCatchUpDistance = 1.5f;
        
        
        public UnityEvent onObjectCatchUp = new UnityEvent();
        public UnityEvent onReachedTarget = new UnityEvent();
        
        private void Start()
        {
            controller ??= GetComponent<AIController>();
            if (catchUpPoint == null)
                catchUpPoint = transform;

        }

        public void StartWalking()
        {
            if (_walkRoutine == null) _walkRoutine = StartCoroutine(Walking());
        }

        //See if its in view
        private IEnumerator Walking()
        {
            Debug.Log("Start Walking");
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            bool nearEnough = false;

            while (distanceToTarget > 0.5f)
            {
                Vector3 position = transform.position;
                float distanceFromObjectThatFollow = Vector2.Distance(catchUpPoint.position, objectThatFollow.transform.position);
                Vector2 direction =  target.position - position;
                direction.Normalize();
                if (nearEnough == false && distanceFromObjectThatFollow < minimumCatchUpDistance)
                {
                    //Move in the direction off
                    controller.SetMoveDirection(direction);
                    nearEnough = true;
                    onObjectCatchUp?.Invoke();
                }
                else if (primaryRenderer.isVisible == false) // Hide the scene view, when testing. 
                {
                    // Character stops moving
                    controller.SetMoveDirection(Vector2.zero);
                    nearEnough = false;
                }

                yield return new WaitForEndOfFrame();
                distanceToTarget = Vector2.Distance(position, target.position);
            }

            Debug.Log("End Walking");
            onReachedTarget?.Invoke();
            transform.position = target.position;
            controller.SetMoveDirection(Vector2.zero);
            _walkRoutine = null;
            yield break;
        }

        public void SetTarget(Transform transform)
        {
            target = transform;
        }
    }
}