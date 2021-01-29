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

        public void SetExpectedSecond(bool state)
        {
            _expectedSecond = state;
        }

        public void ShowWords(HieroGlyph firstWord = null, HieroGlyph secondWord = null)
        {
            if (gameObject.activeInHierarchy == false)
            {
                BubbleStartEffect();
                bubbleAnimator.SetBool("SecondWord", secondWord != null);
            }

            firstRenderer.sprite = firstWord?.talkImage;
            secondWordRenderer.sprite = secondWord?.talkImage;

            Invoke(nameof(BubbleEnd), 1f);
        }

        protected override void BubbleEnd()
        {
            if (_expectedSecond)
            {
                bubbleAnimator.SetBool("SecondWord", true);
            }
            gameObject.LeanCancel();
            base.BubbleEnd();
        }
    }
}