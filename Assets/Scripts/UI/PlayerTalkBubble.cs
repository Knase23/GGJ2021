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
                Debug.Log("Should wait 1");
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
            if (waitForSecondWord && secondWord == null)
            {
                waitForSecondWord = false;
            }
            
        }

        public override void BubbleEnd()
        {
            if (waitForSecondWord)
            {
                Debug.Log("Should wait 2");
                gameObject.LeanCancel();
            }
            else
            {
                AnimationForBubbleGone();
            }
            
            
        }
    }
}
