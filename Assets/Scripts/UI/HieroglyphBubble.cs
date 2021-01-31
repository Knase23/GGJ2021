using System;
using Game.Core;
using UnityEngine;

namespace Game.UI
{
    public class HieroglyphBubble : Bubble
    {
        public SpriteRenderer secondWordRenderer;
        private bool _expectedSecond;

        protected override void Start()
        {
            base.Start();
            secondWordRenderer.sprite = null;
        }

        public virtual void SetExpectedSecond(bool state)
        {
            _expectedSecond = state;

            if (_expectedSecond == false)
            {
                base.BubbleEnd();
            }
        }

        private bool showedSecondWord;
        public  void SetSecondWordAnimation(bool state)
        {
            bubbleAnimator.SetBool("SecondWord", state);
        }
        
        public virtual void ShowWords(HieroGlyph firstWord = null, HieroGlyph secondWord = null)
        {
            if (gameObject.activeInHierarchy == false)
            {
                BubbleStartEffect();
            }

            SetSecondWordAnimation(secondWord != null);
            firstRenderer.sprite = firstWord?.talkImage;
            secondWordRenderer.sprite = secondWord?.talkImage;
            
            BubbleEnd();
        }

        public override void BubbleEnd()
        {
            if(_expectedSecond && completedStart)
                gameObject.LeanCancel();

            base.BubbleEnd();
        }
    }
}