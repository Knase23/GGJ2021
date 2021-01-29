using UnityEngine;

namespace Game.UI
{
    public class Bubble : MonoBehaviour
    {
        public SpriteRenderer firstRenderer;
        
        public Animator bubbleAnimator;
        
        private Vector3 _defaultsScale;

        protected virtual void Start()
        {
            firstRenderer.sprite = null;
            
            gameObject.SetActive(false);
            _defaultsScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }
        protected virtual void BubbleStartEffect()
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
                gameObject.LeanCancel();
            }
            transform.localScale = Vector3.zero;
            gameObject.LeanScale(_defaultsScale, 0.5f).setEaseOutBack();
        }

        protected const float AnimationDuration = 0.5f;
        protected virtual void BubbleEnd()
        {
            gameObject.LeanScale(Vector3.zero, AnimationDuration).setDelay(1.5f).setEaseOutBack()
                .setOnComplete(() => { gameObject.SetActive(false); });
        }
        
    }
}