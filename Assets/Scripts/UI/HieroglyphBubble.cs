using System;
using Game.Core;
using UnityEngine;

namespace Game.UI
{
    public class HieroglyphBubble : MonoBehaviour
    {
        public SpriteRenderer firstWordRenderer;
        public SpriteRenderer secondWordRenderer;
        public Animator bubbleAnimator;

        private Vector3 _defaultsScale;
        private bool _expectedSecond;

        private void Start()
        {
            firstWordRenderer.sprite = null;
            secondWordRenderer.sprite = null;
            gameObject.SetActive(false);
            _defaultsScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        public void SetExpectedSecond(bool state)
        {
            _expectedSecond = state;
        }

        public void UpdateView(Hieroglyph firstWord = null, Hieroglyph secondWord = null)
        {
            if(firstWord is LogicGlyph)
                return;
            
            gameObject.SetActive(true);
            gameObject.LeanScale(_defaultsScale, 0.5f).setEaseOutBack();

            firstWordRenderer.sprite = firstWord?.talkImage;
            secondWordRenderer.sprite = secondWord?.talkImage;

            //Hide after a few seconds if not expecting SecondWord.

            if (secondWord != null)
            {
                bubbleAnimator.SetTrigger("SecondWord");
                gameObject.LeanScale(Vector3.zero, 0.5f).setDelay(2.5f).setEaseOutBack()
                    .setOnComplete(() => { gameObject.SetActive(false); });
            }
            else if(_expectedSecond == false)
            {
                gameObject.LeanScale(Vector3.zero, 0.5f).setDelay(2.5f).setEaseOutBack()
                    .setOnComplete(() => { gameObject.SetActive(false); });
            }
        }
    }
}