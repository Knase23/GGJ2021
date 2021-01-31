using System.Collections;
using System.Collections.Generic;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class PlayerTalkBubble : HieroglyphBubble
    {
        private bool waitForSecondWord;
        
        public override void SetExpectedSecond(bool state)
        {
            if (state)
            {
                waitForSecondWord = true;
            }
            base.SetExpectedSecond(state);
        }

        public override void ShowWords(HieroGlyph firstWord = null, HieroGlyph secondWord = null)
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
            if (waitForSecondWord)
            {
                gameObject.LeanCancel();
                waitForSecondWord = false;
            }
            else
            {
                AnimationForBubbleGone();
            }
            
            
        }
    }
}
