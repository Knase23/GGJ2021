using System;
using UnityEngine;

namespace Game.UI
{
    public class Bubble : MonoBehaviour
    {
        public SpriteRenderer firstRenderer;
        
        public Animator bubbleAnimator;
        
        private Vector3 _defaultsScale;

        public float animationInDuration = 0.5f;
        
        public float animationOutDuration = 0.5f;
        public float animationOutDelay = 1.5f;
        public event Action OnBubbleEnd;
        public event Action OnBubbleStartComplete;
        protected bool completedStart;
        protected bool startedEnd;
        
        protected virtual void Start()
        {
            firstRenderer.sprite = null;
            gameObject.SetActive(false);
            _defaultsScale = transform.localScale;
            transform.localScale = Vector3.zero;
            OnBubbleStartComplete += () => completedStart = true;
        }

        public void StopShowing()
        {
            transform.localScale = Vector3.zero;
            gameObject.LeanCancel();
        }
        
        public virtual void BubbleStartEffect()
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
                gameObject.LeanCancel();
            }
            if (completedStart == false)
            {
                transform.localScale = Vector3.zero;
                gameObject.LeanScale(_defaultsScale, animationInDuration).setEaseOutBack()
                    .setOnComplete(() => OnBubbleStartComplete?.Invoke());
            }
            else
            {
                transform.localScale = _defaultsScale;
            }
        }
        
        public virtual void BubbleEnd()
        {
            if(gameObject.activeInHierarchy == false) return;
            
            if(startedEnd == false) 
                startedEnd = true;
            else if (completedStart)
            {
                //Debug.Log("Bubble End Lean Cancel");
                gameObject.LeanCancel();
                transform.localScale = _defaultsScale;
            }
            //Debug.Log("Bubble End Lean",gameObject);
            AnimationForBubbleGone();
        }

        public void AnimationForBubbleGone()
        {
            gameObject.LeanScale(Vector3.zero, animationOutDuration).setDelay(animationOutDelay).setEaseOutBack()
                .setOnComplete(() =>
                {
                    if(bubbleAnimator.gameObject.activeInHierarchy)
                        bubbleAnimator.Play("Bubble_OneWord");
                    gameObject.SetActive(false);
                    OnBubbleEnd?.Invoke();
                    completedStart = false;
                    startedEnd = false;
                });
        }
        
    }
}