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

        public UnityEvent onObjectCatchUp = new UnityEvent();
        public UnityEvent onReachedTarget = new UnityEvent();
        
        private void Start()
        {
            controller ??= GetComponent<AIController>();
        }

        public void StartWalking()
        {
            if (_walkRoutine == null) _walkRoutine = StartCoroutine(Walking());
        }

        //See if its in view
        private IEnumerator Walking()
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            bool nearEnough = false;

            while (distanceToTarget > 0.5f)
            {
                Vector3 position = transform.position;
                float distanceFromObjectThatFollow = Vector2.Distance(position, objectThatFollow.transform.position);
                Vector2 direction =  target.position - position;
                direction.y = 0;
                direction.Normalize();
                if (nearEnough == false && distanceFromObjectThatFollow < 1.5f)
                {
                    //Move in the direction off
                    controller.SetMoveDirection(direction);
                    nearEnough = true;
                }
                else if (primaryRenderer.isVisible == false) // Hide the scene view, when testing. 
                {
                    // Character stops moving
                    controller.SetMoveDirection(Vector2.zero);
                    nearEnough = false;
                }

                yield return new WaitForEndOfFrame();
                distanceToTarget = Vector2.Distance(transform.position, target.position);
            }

            transform.position = target.position;
            controller.SetMoveDirection(Vector2.zero);
            _walkRoutine = null;
            yield break;
        }
    }
}